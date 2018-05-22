using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace demo
{
    [Route("api/[controller]")]
    
    public class MyCalendarController : Controller
    {
	    private readonly IMediator _mediator;

	    public MyCalendarController(IMediator mediator)
	    {
		    _mediator = mediator;
	    }

        [HttpGet("{id}")]
        public async Task<List<AgendaScheduleItem>> Get(int id) => await _mediator.Send(new GetBranchScheduleItems { Year = id });
    }
}