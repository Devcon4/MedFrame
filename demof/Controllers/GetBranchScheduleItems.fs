namespace demof

open System
open System.Collections.Generic
open System.Linq
open System.Threading
open System.Threading.Tasks
open MediatR
open Microsoft.EntityFrameworkCore

type GetBranchScheduleItems(id: int) =
    interface IRequest<AgendaScheduleItem>
    member this.Id = id

type GetBranchScheduleItemsHandler() =
    interface IRequestHandler<GetBranchScheduleItems, AgendaScheduleItem> with
        member this.Handle (request: GetBranchScheduleItems, cancellationToken: CancellationToken) =
            async { return new AgendaScheduleItem(request.Id, "f#") } |> Async.StartAsTask
