module Count

open Feliz
open Feliz.UseElmish
open Elmish

type Model =
    { Count : int
      IsLoading : bool }
    static member Create(count : int, ?isLoading : bool) =
        { Count = count
          IsLoading = defaultArg isLoading false }

type Msg =
    | Increase
    | Decrease
    | GetCountSuccess of Result<int, exn>
    | GetCountFailure of exn

let init () : Model * Cmd<Msg> =
    Model.Create(count = 0), Cmd.none

let initWithCount (count : int) : Model * Cmd<Msg> =
    Model.Create(count), Cmd.none

let initWithModel (model : Model) : Model * Cmd<Msg> =
    model, Cmd.none

let getCountCmd : Cmd<Msg> =
    Cmd.OfAsync.either Api.remoting.GetCount () GetCountSuccess GetCountFailure

let initLoad () : Model * Cmd<Msg> =
    Model.Create(count = 0, isLoading = true), getCountCmd

let update (msg : Msg) (model : Model) : Model * Cmd<Msg> =
    match msg with
    | Increase -> { model with Count = model.Count + 1 }, Cmd.none
    | Decrease -> { model with Count = model.Count - 1 }, Cmd.none
    | GetCountSuccess result ->
        match result with
        | Ok count -> { model with Count = count; IsLoading = false }, Cmd.none
        | Error error ->
            printfn $"[GetCountSuccess] error: $%s{error.Message}"
            { model with Count = 0; IsLoading = false }, Cmd.none
    | GetCountFailure exn ->
        printfn $"[GetCountFailure] exn: $%s{exn.Message}"
        { model with Count = 0; IsLoading = false }, Cmd.none


let increaseCountButton =
    "Increase count"

let decreaseCountButton =
    "Decrease count"

type Count =
    [<ReactComponent>]
    static member CountComponent
        (
            ?modelOpt : {| Count : int; IsLoading : bool |},
            ?dispatchOpt : Msg -> unit
        ) : Fable.React.ReactElement =
        let innerModel, innerDispatch =
            match modelOpt, dispatchOpt with
            | Some model, Some dispatch -> Model.Create(model.Count, model.IsLoading), dispatch
            | None, Some dispatch -> Model.Create(0), dispatch
            | Some model, None ->
                React.useElmish (
                    initWithModel <| Model.Create(model.Count, model.IsLoading),
                    update,
                    [| box model.Count; box model.IsLoading |]
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
                    prop.disabled innerModel.IsLoading
                    prop.onClick (fun _ -> Decrease |> innerDispatch)
                    prop.text "-"
                ]
                if innerModel.IsLoading then
                    Html.button [
                        prop.classes [
                            "btn btn-circle loading"
                            "bg-fable-blue-400 dark:bg-fable-blue-800"
                        ]
                        prop.onClick (fun _ -> ())
                        prop.text System.String.Empty
                    ]
                else
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
                    prop.disabled innerModel.IsLoading
                    prop.onClick (fun _ -> Increase |> innerDispatch)
                    prop.text "+"
                ]
            ]
        ]

let view (model : Model) (dispatch : Msg -> unit) : Fable.React.ReactElement =
    Count.CountComponent({| Count = model.Count; IsLoading = model.IsLoading |}, dispatch)
