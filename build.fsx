// include Fake lib
#r @"packages/FAKE.Core/tools/FakeLib.dll"
#r @"packages/FAKE.Dotnet/tools/Fake.Dotnet.dll"
open Fake
open Fake.Dotnet

// Properties
let artifactsDir = "artifacts"
DefaultDotnetCliDir <- "C:\\Program Files\\dotnet"
let DotnetExe = DefaultDotnetCliDir @@ "dotnet.exe"

// Helpers

let DotnetLambdaDeploy proj =
    let p = {
        CustomParams = None
        DotnetCliPath = DotnetExe
        WorkingDirectory = directory proj
    }
    Dotnet p ("lambda deploy-serverless") |> ignore

// Targets
Target "Clean" (fun _ ->
    !! artifactsDir ++ "src/*/bin"  ++ "src/*/obj"
        |> DeleteDirs
)

Target "BuildProjects" (fun _ ->
    !! "src/*/project.json" 
        |> Seq.iter(fun proj ->  

            // restore project dependencies
            DotnetRestore id proj

            // build project and produce outputs
            DotnetCompile id proj
        )
)

Target "DeployLambda" (fun _ ->
    "src/FSharpLambda/project.json"
    |> DotnetLambdaDeploy
)

Target "Default" <| DoNothing

// Dependencies
"Clean"
    ==> "BuildProjects"
    ==> "DeployLambda"
    ==> "Default"

// start build
RunTargetOrDefault "Default"
