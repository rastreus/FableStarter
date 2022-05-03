# FableStarter

<img src="src/fable_logo.png" alt="Fable Logo" width="245.8" />

An Elmish F# [Fable](https://fable.io) template that uses [Tailwind](https://tailwindcss.com) for styling, the [Vite](https://vitejs.dev) bundler and [Vitest](https://vitest.dev).

_Updated with Tailwind CSS v3. Check out their release [blog post](https://tailwindcss.com/blog/tailwindcss-v3) to learn more._

## Install pre-requisites

You'll need to install the following pre-requisites in order to use the Fable Starter template:

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Node](https://nodejs.org/en/download/)
- [Yarn](https://classic.yarnpkg.com/lang/en/)

## Getting Started with FableStarter

Use [degit](https://github.com/Rich-Harris/degit) to set up the template:

`npx degit rastreus/FableStarter <your project name>`

Run the following commands from the root of the project directory:

0. `cd <your project name>`

1. `dotnet tool restore`

2. `dotnet paket install`

3. `yarn install`

4. `dotnet build`

5. `yarn dev:fable`

6. Open a brower to `http://localhost:3000`

## Running Tests

Tests can be run by Vitest in its watch mode. Open a separate terminal instance and run `yarn test`.

Check out [Fable.Expect](https://github.com/fable-compiler/Fable.Expect) and [Fable.Mocha](https://github.com/Zaid-Ajaj/Fable.Mocha) to learn more about defining tests.

## Other Templates

If this template isn't what you're wanting, there are other templates. Some only set up a frontend Fable project and then others are full-stack web app templates that include a F# backend:

- [fable/compiler/fable-templates](https://github.com/fable-compiler/fable-templates)
- [albertwoo/FablePlayground](https://github.com/albertwoo/FablePlayground)
- [Bjorn-Strom/elmish-fss-template](https://github.com/Bjorn-Strom/elmish-fss-template)
- [SAFE-Stack/SAFE-template](https://github.com/SAFE-Stack/SAFE-template)
- [Zaid-Ajaj/SAFE.React](https://github.com/Zaid-Ajaj/SAFE.React)
- [Dzoukr/SAFEr.Template](https://github.com/Dzoukr/SAFEr.Template)

Additional templates can be found at [https://fable.io/resources.html#Templates](https://fable.io/resources.html#Templates)
