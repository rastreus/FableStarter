module Main

// Entry point must be in a separate file
// for Vite Hot Reload to work

module private MainElmish =

    open Elmish
    open Elmish.React
    #if DEBUG
    open Elmish.Debug
    open Elmish.HMR
    #endif

    let mkProgram () =
        // We pass a dummy function as `view` because we don't need it
        let view = fun _ _ -> ()
        Program.mkProgram AppElmish.init AppElmish.update view
        #if DEBUG
        |> Program.withConsoleTrace
        |> Program.withDebugger
        #endif

open Fable.Core
open Browser
open Fable.React
open Feliz
open Feliz.UseElmish
open MainElmish

[<ReactComponent>]
let Main () : Fable.React.ReactElement =
    let model, dispatch = React.useElmish (mkProgram, [||])
    AppComponent (model, dispatch)

let root = ReactDOM.createRoot (document.getElementById ("elmish-app"))
root.render(Main())
