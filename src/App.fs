module App

open Feliz
open Elmish
open Tailwind

[<ReactComponent(import = "FableLogo", from = "./FableLogo.jsx")>]
let FableLogo() = React.imported()

type Model = { Text : string }

type Msg = | NoOp

let init () : Model * Cmd<Msg> =
    { Text = "FableStarter" }, Cmd.none

let update (msg : Msg) (model : Model) : Model * Cmd<Msg> =
    match msg with
    | NoOp -> model, Cmd.none

let view (_ : Model) (dispatch : Msg -> unit) =
    Html.div [
        prop.className [
            tw.``w-full``
            tw.``h-screen``
            tw.``bg-fable-blue-500``
            tw.``dark:bg-fable-blue-900``
            tw.``flex``
            tw.``flex-col``
            tw.``justify-center``
            tw.``items-center``
        ]
        prop.children [
            FableLogo()
        ]
    ]
