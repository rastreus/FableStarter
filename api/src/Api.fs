module Api

open Microsoft.Extensions.Logging
open Microsoft.Extensions.Configuration
open Shared
open FSharp.Data.LiteralProviders

/// An implementation of the Shared IApi protocol.
/// Can require ASP.NET injected dependencies in the constructor
/// and uses the Build() function to return value of `IApi`.
type Remoting
    (
        logger : ILogger<Remoting>,
        config : IConfiguration
    ) =
    member this.GetCount() : Async<Result<int, exn>> =
        async {
            logger.LogInformation("[GetCount]")
            do! Async.Sleep 3000
            return (Ok 42)
        }
    member this.Build() : IApi =
        { GetCount = this.GetCount }
