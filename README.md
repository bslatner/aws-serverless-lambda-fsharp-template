# Template for F# Serverless Application on AWS Lambda

As of this writing, the tooling provided by Amazon for Visual Studio only 
supports C#. This template attempts to bridge that gap.

## What's Included

The project FSharpLambda provides everything you need to get started deploying
serverless F# applications on AWS.

The project consists of:

* `aws-lambda-tools-defaults.json`

  This file contains the default values used by the AWS Lambda build tool.
  You can specify any of these parameters on the command line, but this file
  makes it easier. For your own project, you should edit the file and make sure
  to provide:

  * `profile`

    The name of the AWS profile used to deploy the application. See 
    [Getting Started with the AWS SDK for .NET](http://docs.aws.amazon.com/sdk-for-net/v2/developer-guide/net-dg-setup.html) for
    information about profiles.

  * `region`

    The AWS region to deploy the application to, e.g. `us-east-1`.

  * `s3-bucket`

    The S3 bucket to which the deployment tool will upload your code.

* `project.json`

  The project file that contains all the proper references to AWS NuGet packages and 
  the AWS Lambda tooling.

* `serverless.template`

  An AWS CloudFormation template for deploying your application. This file defines
  all of the URLs in your application and the Lambda handlers for each one.

* `Lambda.fs`

  The code that handles individual requests.

## Getting Started

* Clone this repository
* Edit `aws-lambda-tools-defaults.json`
* Run `build.cmd` from the root

  **NOTE**: I welcome anyone who is working on OSX or Linux to send a pull request
  that will build and run this example on a platform other than Windows.

Once the build completes, you'll have a deployed CloudFormation stack for your
application.

The CloudFormation template creates:

* An API Gateway RestApi
* Two stages inside the API called `Prod` and `Stage`
* Methods for `/` and `/foo` in each stage
* Two Lambda functions for handling `/` and `/foo`

Once your stack is deployed, you can use PowerShell to get the URL for your 
serverless application:

```powershell
Initialize-AWSDefaults -ProfileName [your profile name] -Region [deployed region]
Get-AGRestApiList
```

This will produce a list of rest APIs in the AWS API Gateway. One of those should
be FSharpLambda.

Get the ID of the API, and then you should be able to test by visiting the following
URLS:

* https://[your ID].execute-api.[your region].amazonaws.com/Prod/
* https://[your ID].execute-api.[your region].amazonaws.com/Prod/foo

So, for example, if your API ID is foobar and you deployed to the us-east-1 region:

* https://foobar.execute-api.us-east-1.amazonaws.com/Prod/
* https://foobar.execute-api.us-east-1.amazonaws.com/Prod/foo
