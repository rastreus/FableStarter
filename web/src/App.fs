[<AutoOpen>]
module App

open Feliz

[<ReactComponent>]
let AppComponent (model : AppElmish.Model, dispatch : AppElmish.Msg -> unit) : Fable.React.ReactElement =
    Html.div [
        prop.classes [
            "w-screen h-screen"
            "bg-fable-blue-500 dark:bg-fable-blue-900"
            "flex flex-col"
            "justify-center items-center"
            "gap-4"
        ]
        prop.children [
            React.strictMode [
                FableLogo.FableLogoComponent()
                Count.CountComponent({|
                    Count = model.CountModel.Count
                    IsLoading = model.CountModel.IsLoading
                |},
                (AppElmish.Msg.CountMsg >> dispatch))
            ]
        ]
    ]
