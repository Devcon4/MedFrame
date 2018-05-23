namespace Demof

open System.Collections.Generic
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open System
open MediatR
open demof


[<Route("api/[controller]")>]
type MyCalendarController(mediator: IMediator) =
    inherit Controller()
    let _mediator = mediator
    [<HttpGet("{id}")>]
    member this.Get id = _mediator.Send(new GetBranchScheduleItems(id))
