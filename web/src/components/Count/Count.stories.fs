module CountStories

open Fable.Core.JsInterop
open Count

let defaultExport =
    createObj [
        "title" ==> "Count"
        "component" ==> Count.CountComponent
    ]

exportDefault defaultExport

let Five () =
    Count.CountComponent({| Count = 5; IsLoading = false |})
