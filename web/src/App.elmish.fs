module AppElmish

open Elmish

type Model =
    { Text : string
      CountModel : CountElmish.Model }

type Msg =
    | CountMsg of CountElmish.Msg
    | NoOp

let init () : Model * Cmd<Msg> =
    let countModel, countCmd =
        CountElmish.initLoad ()
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
                CountElmish.update countMsg model.CountModel
            { model with CountModel = newCountModel }, Cmd.map CountMsg countCmd
    | NoOp -> model, Cmd.none
