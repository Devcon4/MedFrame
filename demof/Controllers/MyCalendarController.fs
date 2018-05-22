namespace demof

open System.Collections.Generic
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open System
open MediatR

[<Route("api/[controller]")>]
type MyCalendarController(mediator: IMediator) =
    inherit Controller()
    let _mediator = mediator
    member this.Get id = async { return _mediator.Send |> new GetBranchScheduleItems(id) }
