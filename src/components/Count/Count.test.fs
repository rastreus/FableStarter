namespace Tests

module CountTest =

    open Components.Count
    open Feliz
    open Elmish
    open Expect
    open Expect.Dom
    open Expect.Elmish
    open WebTestRunner

    let private mount (container : Browser.Types.HTMLElement) (element : Fable.React.ReactElement) : unit =
        ReactDOM.render(element, container)

    describe "Count" <| fun () ->
        it "Increase count" <| fun () -> promise {
            // Initialize the Elmish app with Program.mountAndTest,
            // this returns a container that also gives access to the Elmish model.
            // `mount` is a function that renders the result of `view`
            //  onto the container's element (e.g. ReactDom.render)
            use! container =
                Program.mkProgram init update CountComponent
                |> Program.mountAndTest mount

            // Get the element from the container
            let el = container.El

            // We can get the form elements using the aria labels, same way as screen readers will do
            el.getButton(increaseCountButton).click()

            // Get the updated model and confirm it contains the increased count
            container.Model.Count
            |> Expect.equal 1
        }
