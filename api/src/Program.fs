module Program

open Saturn
open Giraffe
open Shared
open Fable.Remoting.Server
open Fable.Remoting.Giraffe
open Microsoft.Extensions.DependencyInjection
open Microsoft.AspNetCore.Http

let tryGetEnv =
    System.Environment.GetEnvironmentVariable
    >> function
        | null
        | "" -> None
        | x -> Some x

let port =
    "API_PORT"
    |> tryGetEnv
    |> Option.map uint16
    |> Option.defaultValue 5178us

let publicPath =
    System.IO.Path.GetFullPath "../../web/src/public"

let remoting =
    Remoting.createApi ()
    |> Remoting.fromContext (fun (ctx : HttpContext) ->
        ctx.GetService<Api.Remoting>().Build()
    )
    |> Remoting.withRouteBuilder routerPaths
    |> Remoting.buildHttpHandler

let test =
    router { get "/api/ping" (text <| "pong" + System.Environment.NewLine) }

let webApp =
    choose [
        remoting
        test
    ]

let serviceConfig (services : IServiceCollection) =
    services
        .AddSingleton<Api.Remoting>()
        .AddLogging()

let app =
    application {
        url ("http://0.0.0.0:" + port.ToString() + "/")
        use_cors
            "Any"
            (fun policy ->
                policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
                |> ignore
            )
        use_router webApp
        memory_cache
        use_static publicPath
        use_gzip
        service_config serviceConfig
    }

run app
