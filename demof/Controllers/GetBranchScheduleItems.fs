
open System
open System.Collections.Generic
open MediatR
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc

type AgendaScheduleItem (id: int) = 
    member this.id = id
    

type GetBranchScheduleItemsHandler =
    let agendaType id = { new Object() with member id = id }
    let Handle request cancellationToken = async {
        return [new AgendaScheduleItem(request)]
    }



[<Route("api/[controller]")>]
type MyCalendarController(mediator: IMediator) =
    inherit Controller()
    let _mediator = mediator
    member this.Get id = async { return _mediator.Send |> new GetBranchScheduleItems(id) }
