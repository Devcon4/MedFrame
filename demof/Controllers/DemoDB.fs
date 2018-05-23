namespace demof

open System
open Microsoft.EntityFrameworkCore

type DemoDB(options: DbContextOptions<DemoDB>) =
    inherit DbContext(options)
