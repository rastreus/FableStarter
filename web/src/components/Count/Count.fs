[<AutoOpen>]
module Count

open Feliz
open Feliz.UseElmish
open CountElmish

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
