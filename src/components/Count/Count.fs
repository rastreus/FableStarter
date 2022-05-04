module Count

open Feliz
open Feliz.UseElmish
open Elmish
open Thoth.Json

type Model =
    { Count : int }
    static member Create(count : int) = { Count = count }
    static member Decoder : Decoder<Model> =
        Decode.object (fun get ->
            { Count = get.Required.Field "count" Decode.int }
        )
    static member Encoder(model : Model) =
        Encode.object [
            "count", Encode.int model.Count
        ]

type Msg =
    | Increase
    | Decrease
    | InitLoad
    | InitLoadResult of Result<Model, string>

let init () : Model * Cmd<Msg> =
    Model.Create(count = 0), Cmd.none

let initWithCount (count : int) : Model * Cmd<Msg> =
    Model.Create(count = count), Cmd.none

let initLoad () : Async<Result<Model, string>> =
    async {
        do! Async.Sleep 1000
        let json =
            """{"count":0,"unusedField":"notDecoded"}"""
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

let increaseCountButton =
    "Increase count"

let decreaseCountButton =
    "Decrease count"

type Count =
    [<ReactComponent>]
    static member CountComponent
        (
            ?modelOpt : {| Count : int |},
            ?dispatchOpt : Msg -> unit
        ) : Fable.React.ReactElement =
        let innerModel, innerDispatch =
            match modelOpt, dispatchOpt with
            | Some model, Some dispatch -> Model.Create(model.Count), dispatch
            | None, Some dispatch -> Model.Create(0), dispatch
            | Some model, None ->
                React.useElmish (
                    initWithCount model.Count,
                    update,
                    [| box model.Count |]
                )
            | None, None -> React.useElmish (initWithCount 0, update, [||])
        Html.div [
            prop.classes [
                "w-full h-auto"
                "flex flex-row"
                "justify-center items-center"
                "gap-4"
            ]
            prop.children [
                Html.button [
                    prop.ariaLabel decreaseCountButton
                    prop.classes [
                        "w-10 h-10"
                        "text-black dark:text-white"
                        "bg-fable-blue-100 dark:bg-fable-blue-600"
                        "rounded-full"
                    ]
                    prop.onClick (fun _ -> Decrease |> innerDispatch)
                    prop.text "-"
                ]
                Html.div [
                    prop.classes [
                        "text-lg font-mono"
                        "text-black dark:text-fable-blue-400"
                    ]
                    prop.text (string innerModel.Count)
                ]
                Html.button [
                    prop.ariaLabel increaseCountButton
                    prop.classes [
                        "w-10 h-10"
                        "text-black dark:text-white"
                        "bg-fable-blue-100 dark:bg-fable-blue-600"
                        "rounded-full"
                    ]
                    prop.onClick (fun _ -> Increase |> innerDispatch)
                    prop.text "+"
                ]
            ]
        ]

let view (model : Model) (dispatch : Msg -> unit) : Fable.React.ReactElement =
    Count.CountComponent({| Count = model.Count |}, dispatch)
