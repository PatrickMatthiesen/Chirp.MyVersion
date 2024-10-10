# My version of the twitter clone project for BDSA

## Introduction

This is a Razor Pages project for the course BDSA at the IT University of Copenhagen.

I would like to reference to be clear that i do not recommend using Razor Pages for a project like this, but instead use a more modern and better supported framework like Blazor.

## Local setup

To run the project you need to have the following installed:
dotnet core 8.0 or higher

You will need to set 3 environment variables or preferably user secrets for the project to run.

```bash
dotnet user-secrets set -p Chirp.Server "ConnectionStrings:ChirpDB" "<Set a connection string to a database>"
dotnet user-secrets set -p Chirp.Server "Authentication:github:clientId" "<Set a github client id>"
dotnet user-secrets set -p Chirp.Server "Authentication:github:clientSecret" "<Set a github client secret>"
```

To run the project you need to navigate to the project folder and run the following command:

```bash
dotnet run --project Chirp.Server
```

## Deployment to Azure

To deploy the project to Azure you need to have an Azure account and have the Azure CLI installed.

To deploy the project to Azure you need to run the following commands:

1. Publish the project

```sh
dotnet publish --runtime linux-x64 -c Release
```

2. Zip the publish folder

```sh
# Bash
zip -r Chrip.zip Chirp.Server/bin/Release/net8.0

# PowerShell
Compress-Archive -Path Chirp.Server\bin\Release\net8.0 -DestinationPath Chirp.Server\bin\Release\net8.0.zip
```

3. Deploy the project to Azure

I am assuming you have already created a web app in Azure else look in section 

```sh
az webapp deploy --name <app-name> --resource-group <resource-group> --src-path Chirp.Server/bin/Release/net8.0.zip --type zip
```

1. Set the environment variables

```sh
az webapp config appsettings set -n <app-name> --settings "ConnectionStrings__ChirpDB=<Set a connection string to a database>"
az webapp config appsettings set -n <app-name> --settings "Authentication__github__clientId=<Set a github client id>"
az webapp config appsettings set -n <app-name> --settings "Authentication__github__clientSecret=<Set a github client secret>"
```

## Making a web app in Azure

To make a web app in Azure you need to run the following command:

```sh
az webapp create --name <app-name> --resource-group <resource-group> --plan <app-service-plan> --runtime "DOTNET|8.0"
```