module App

open Feliz
open Elmish
open Components

[<ReactComponent(import = "FableLogo", from = "./FableLogo.jsx")>]
let FableLogo () = React.imported ()

type Model =
    { Text : string
      CountModel : Count.Model }

type Msg =
    | CountMsg of Count.Msg
    | NoOp

let init () : Model * Cmd<Msg> =
    let countModel, countCmd = Count.init()
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
            { model with CountModel = newCountModel },
            Cmd.map CountMsg countCmd
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
            FableLogo()
            Count.CountComponent model.CountModel (CountMsg >> dispatch)
        ]
    ]
