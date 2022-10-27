module Api

open Fable.Remoting.Client

let remoting =
    Remoting.createApi ()
    |> Remoting.withRouteBuilder Shared.routerPaths
    |> Remoting.buildProxy<Shared.IApi>
