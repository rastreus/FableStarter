module CountElmish

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
    Model.Create(count = 0, isLoading = false),
    Cmd.none
    // getCountCmd // Fable.Remoting

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
