module Shared

let notEmpty =
    not << System.String.IsNullOrWhiteSpace

/// Defines how routes are generated on server and mapped from client
let routerPaths typeName method = sprintf "/api/%s" method

/// A type that specifies the communication protocol between client and server
/// to learn more, read the docs at https://zaid-ajaj.github.io/Fable.Remoting/src/basics.html
type IApi = {
    GetCount : unit -> Async<Result<int, exn>>
}
