# My Fridge Blazor App
It's an ASP.NET Core Application to manage your Fridge stock using different list items. 

This is complete Blazor Application as example and it includes custom components, services, WebAPI, and Entity Framework Core.

## Design
- The application has four projects:
  * Client: where we have all related with the presentantion layer (pages, components, css, etc)
  * Data: EF Context + Migrations
  * Server: Controllers / API
  * Shared: Models that we are sharing between Client and Server

## Installation steps
To install you only have to:

- Execute a prompt in the Project folder with:
  * > dotnet build

- Go to MyFridge.Server folder and execute EF Migrations:
  * > dotnet ef database update

## Execution steps
- Go to MyFridge\MyFridge.Server\bin\Debug\netcoreapp2.1\ and execute:
  * > dotnet MyFridge.Server.dll

## GIT Repository
https://github.com/tdtorre/MyFridge
