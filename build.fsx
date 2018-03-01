// include Fake lib
#r @"packages/FAKE/tools/FakeLib.dll"
open Fake
open Fake.Core
open Fake.Core.Globbing.Operators
open Fake.Core.TargetOperators
open Fake.DotNet.Cli

// Properties
let artifactsDir = "artifacts"

// Helpers

let DotNetLambdaDeploy proj =
    DotNet (fun p -> { p with WorkingDirectory = directory proj }) "lambda" "deploy-serverless"
    |> ignore

// Targets
Target.Create "Initialize" <| fun _ ->
    DotNetCliInstall (fun p -> { p with Version = DotNetCliVersion.Version "2.0.0" })

Target.Create "Clean" <| fun _ ->
    !! artifactsDir ++ "src/*/bin"  ++ "src/*/obj"
        |> DeleteDirs

Target.Create "BuildProjects" <| fun _ ->
    !! "src/*/*.fsproj" 
        |> Seq.iter(fun proj ->  
            // build project and produce outputs
            DotNetCompile id proj
        )

Target.Create "DeployLambda" <| fun _ ->
    "src/FSharpLambda/FSharpLambda.fsproj"
    |> DotNetLambdaDeploy


Target.Create "Default" <| Target.DoNothing

// Dependencies
"Initialize"
    ==> "Clean"
    ==> "BuildProjects"
    ==> "DeployLambda"
    ==> "Default"

// start build
Target.RunOrDefault "Default"
