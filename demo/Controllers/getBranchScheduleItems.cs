using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace demo
{
    [AllowedRoles(Roles.User, Roles.All)]
	public class GetBranchScheduleItems : IRequest<List<AgendaScheduleItem>>
	{
		public int Year { get; set; }
	}

	public class GetBranchScheduleItemsHandler : IRequestHandler<GetBranchScheduleItems, List<AgendaScheduleItem>>
	{

		
		// private readonly IAuthenticatedUser _user;

        public GetBranchScheduleItemsHandler() {
			// _user = user;
		}

        public async Task<List<AgendaScheduleItem>> Handle(GetBranchScheduleItems request, CancellationToken cancellationToken)
		{
			return new List<AgendaScheduleItem>(){ new AgendaScheduleItem { name="Item-1"} };
		}
    }

}