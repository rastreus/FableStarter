module App

open Feliz
open Elmish

[<ReactComponent(import = "FableLogo", from = "./FableLogo.jsx")>]
let FableLogo () = React.imported ()

type Model =
    { Text : string
      Count : int }

type Msg =
    | Increase
    | Decrease
    | NoOp

let init () : Model * Cmd<Msg> =
    { Text = "FableStarter"
      Count = 0 },
    Cmd.none

let update (msg : Msg) (model : Model) : Model * Cmd<Msg> =
    match msg with
    | Increase -> { model with Count = model.Count + 1 }, Cmd.none
    | Decrease -> { model with Count = model.Count - 1 }, Cmd.none
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
                prop.className
                    "w-full h-auto flex flex-row justify-center items-center gap-4"
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
                        prop.classes [
                            "text-lg"
                            "text-black dark:text-white"
                        ]
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
