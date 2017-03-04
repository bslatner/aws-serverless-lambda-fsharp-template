namespace FSharpLambda

open System.Collections.Generic
open System.Net
open Amazon.Lambda.Core
open Amazon.Lambda.APIGatewayEvents

[<assembly:LambdaSerializer(typeof<Amazon.Lambda.Serialization.Json.JsonSerializer>)>]

do ()

module Program =

    let Home(request : APIGatewayProxyRequest, context : ILambdaContext): APIGatewayProxyResponse = 
        context.Logger.LogLine("Request for Home")

        let headers = Dictionary<string, string>()
        headers.Add("Content-Type", "text/plain")
        let response = APIGatewayProxyResponse()
        response.StatusCode <- int HttpStatusCode.OK
        response.Body <- "Hello from home page (/)"
        response.Headers <- headers

        response

    let Foo(request : APIGatewayProxyRequest, context : ILambdaContext): APIGatewayProxyResponse = 
        context.Logger.LogLine("Request for Foo")

        let headers = Dictionary<string, string>()
        headers.Add("Content-Type", "text/plain")
        let response = APIGatewayProxyResponse()
        response.StatusCode <- int HttpStatusCode.OK
        response.Body <- "Hello from the foo page (/foo)"
        response.Headers <- headers

        response
