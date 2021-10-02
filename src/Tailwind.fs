module Tailwind

open Zanaptak.TypedCssClasses

// https://fsprojects.github.io/FSharpLint/how-tos/rule-suppression.html
// fsharplint:disable TypeNaming
type tw =
    CssClasses<
        "./src/index.css",
        Naming.Verbatim,
        commandFile = "node",
        argumentPrefix = "tailwind-process.js tailwind.config.js"
    >
// fsharplint:enable
