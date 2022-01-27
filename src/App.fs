module App

open Feliz
open Elmish

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
            "w-full"
            "h-screen"
            "bg-fable-blue-500"
            "dark:bg-fable-blue-900"
            "flex"
            "flex-col"
            "justify-center"
            "items-center"
        ]
        prop.children [
            FableLogo()
        ]
    ]
