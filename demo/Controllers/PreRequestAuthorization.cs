using System.Linq;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;

namespace demo
{
    public class PreRequestAuthorization<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly IAuthenticatedUser _user;

        public PreRequestAuthorization(IAuthenticatedUser user)
        {
            _user = user;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var roles = AllowedRolesAttribute.GetCustomAttributes(request.GetType(), true).Cast<AllowedRolesAttribute>().SelectMany(a => a.Roles).Union(new[] { Roles.SuperUser }).ToList();

            if (roles.Count() > 0)
            {
                _user.DemandAnyRole(roles);
            }
            else
            {
                throw new AuthenticationException($"The request '{request.GetType().Name}' has not been mapped to any roles.");
            }
            return Task.FromResult(0);
        }
    }
}