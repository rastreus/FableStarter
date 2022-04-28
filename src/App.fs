module App

open Feliz
open Elmish
open Thoth.Json

[<ReactComponent(import = "FableLogo", from = "./FableLogo.jsx")>]
let FableLogo () = React.imported ()

type Model =
    { Text : string
      Count : int }
    static member Create(text : string, count : int) =
        { Text = text
          Count = count }
    static member Decoder : Decoder<Model> =
        Decode.object (fun get ->
            { Text = get.Required.Field "text" Decode.string
              Count = get.Required.Field "count" Decode.int }
        )
    static member Encoder(model : Model) =
        Encode.object [
            "text", Encode.string model.Text
            "count", Encode.int model.Count
        ]

type Msg =
    | Increase
    | Decrease
    | InitLoad
    | InitLoadResult of Result<Model, string>
    | NoOp

let init () : Model * Cmd<Msg> =
    Model.Create(text = System.String.Empty, count = 0), Cmd.ofMsg InitLoad

let initLoad () : Async<Result<Model, string>> =
    async {
        do! Async.Sleep 1000
        let json =
            """{"text":"FableStarter","count":5,"unusedField":"notDecoded"}"""
        let decodeResult =
            Decode.fromString Model.Decoder json
        return decodeResult
    }

let update (msg : Msg) (model : Model) : Model * Cmd<Msg> =
    match msg with
    | Increase -> { model with Count = model.Count + 1 }, Cmd.none
    | Decrease -> { model with Count = model.Count - 1 }, Cmd.none
    | InitLoad -> model, Cmd.OfAsync.perform initLoad () InitLoadResult
    | InitLoadResult result ->
        match result with
        | Ok newModel -> newModel, Cmd.none
        | Error error ->
            printfn $"[InitLoadResult] error: $%s{error}"
            model, Cmd.none
    | NoOp -> model, Cmd.none

let view (model : Model) (dispatch : Msg -> unit) =
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
            Html.div [
                prop.classes [
                    "w-full h-auto"
                    "flex flex-row"
                    "justify-center items-center"
                    "gap-4"
                ]
                prop.children [
                    Html.button [
                        prop.classes [
                            "w-10 h-10"
                            "text-black dark:text-white"
                            "bg-fable-blue-100 dark:bg-fable-blue-500"
                            "rounded-full"
                        ]
                        prop.onClick (fun _ -> Decrease |> dispatch)
                        prop.text "-"
                    ]
                    Html.div [
                        prop.className "text-lg text-white font-mono"
                        prop.text (string model.Count)
                    ]
                    Html.button [
                        prop.classes [
                            "w-10 h-10"
                            "text-black dark:text-white"
                            "bg-fable-blue-100 dark:bg-fable-blue-500"
                            "rounded-full"
                        ]
                        prop.onClick (fun _ -> Increase |> dispatch)
                        prop.text "+"
                    ]
                ]
            ]
        ]
    ]
