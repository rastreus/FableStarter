module App

open Feliz
open Elmish

[<ReactComponent(import = "FableLogo", from = "./FableLogo.jsx")>]
let FableLogo () = React.imported ()

type Model =
    { Text : string
      CountModel : Count.Model }

type Msg =
    | CountMsg of Count.Msg
    | NoOp

let init () : Model * Cmd<Msg> =
    let countModel, countCmd =
        Count.initLoad ()
    { Text = "FableStarter"
      CountModel = countModel },
    Cmd.batch [
        Cmd.map CountMsg countCmd
    ]

let update (msg : Msg) (model : Model) : Model * Cmd<Msg> =
    match msg with
    | CountMsg countMsg ->
        match countMsg with
        | _ ->
            let newCountModel, countCmd =
                Count.update countMsg model.CountModel
            { model with CountModel = newCountModel }, Cmd.map CountMsg countCmd
    | NoOp -> model, Cmd.none

let view (model : Model) (dispatch : Msg -> unit) : Fable.React.ReactElement =
    Html.div [
        prop.classes [
            "w-full h-screen"
            "bg-fable-blue-500 dark:bg-fable-blue-900"
            "flex flex-col"
            "justify-center items-center"
            "gap-4"
        ]
        prop.children [
            // https://github.com/Zaid-Ajaj/Feliz/blob/ba7b03cbc07e4ce0375809f925273427fad640f5/public/Feliz/React/StrictMode.md
            React.strictMode [
                FableLogo()
                Count.view model.CountModel (CountMsg >> dispatch)
            ]
        ]
    ]
