using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Authentication;
using System.Security.Principal;

namespace demo
{
    public static class Roles
	{
		public const string User = "IUSER";
		public const string SuperUser = "ISUPER";

		public const string All = "*";
	}

	public interface IUserId {
		int Id { get; set; }
	}

	public interface IAuthenticatedUser : IUserId {
		IPrincipal Principal { get; }
		HashSet<string> UserRoles { get; }
		HashSet<int> Organizations { get; }
		bool IsEpidApprover { get; }
		bool IsStaffAssignmentApprover { get; }
		void DemandAnyRole(IEnumerable<string> roles);
	}

	[Injectable]
	public class AuthenticatedUser : IAuthenticatedUser
	{
		public IPrincipal Principal { get; }

		public int Id { get; set; }
		public HashSet<string> UserRoles { get; }
		public HashSet<int> Organizations { get; }
		public bool IsEpidApprover { get; }
		public bool IsStaffAssignmentApprover { get; }

		private static readonly HashSet<string> KnownRoles;

		static AuthenticatedUser() {
			KnownRoles = new HashSet<string>(
				typeof(Roles).GetFields(BindingFlags.Public | BindingFlags.Static)
					.Where(f => f.FieldType == typeof(string))
					.Select(f => (string) f.GetValue(null))
					.Where(r => r != Roles.All)
			);
		}

		public AuthenticatedUser(IPrincipal principal)
		{
			Principal = principal;

			if (string.Equals(principal.Identity.Name, "TaskRunnerUser", StringComparison.OrdinalIgnoreCase)) {
				UserRoles = new HashSet<string> {Roles.SuperUser};
				Organizations = new HashSet<int>();
			}
			else {
				using (var db = new DemoDB()) {
					var user = db.Users
						.Where(u => u.IsActive && u.UserRoles.AsQueryable().Any(IsActiveRole))
						.Where(u => u.DomainUserName == principal.Identity.Name)
						.Select(u => new {
							u.Id,
							Roles = u.UserRoles.AsQueryable()
								.Where(IsActiveRole)
								.Select(r => r.RoleCode)
								.Distinct()
								.ToList(),
						})
						.FirstOrDefault();

					if (user == null) {
						throw new AuthenticationException($"The user {principal.Identity.Name} is not authorized");
					}

					if (user.Roles.Count() == 0) {
						throw new AuthenticationException($"The user {principal.Identity.Name} does not have any active roles");
					}

					Id = user.Id;
					UserRoles = new HashSet<string>(user.Roles.Intersect(KnownRoles));
				}
			}
		}

		public void DemandAnyRole(IEnumerable<string> roles)
		{
			if (!roles.Any(r => r == Roles.All || UserRoles.Contains(r)))
			{
				throw new AuthenticationException();
			}
		}

		public static readonly Expression<Func<UserRole, bool>> IsActiveRole = r => r.StartDate < DateTime.Now && (r.EndDate == null || r.EndDate > DateTime.Now);
	}
}