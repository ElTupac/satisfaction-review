# satisfaction-review dotnet
This is another app made with .net. Just a simple customer review for my pages, practicing again database connection, client asynchronous responses and so on.

This project is configured to be build as docker image and deployed on heroku, but if you want to use your own you can configure really easy some things and use it where you want with whatever database you want.
For running locally you have to remember to add all the packages needed for the build and run of the project. Those are:
* dotnet tool install --global dotnet-ef
* dotnet tool install --global dotnet-aspnet-codegenerator
* dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
* dotnet add package Microsoft.EntityFrameworkCore.Design
* dotnet add package Microsoft.Extensions.Logging.Debug
* dotnet add package Microsoft.EntityFrameworkCore.SQLite ``If you will use SQLite as database``
* dotnet add package Microsoft.EntityFrameworkCore.SqlServer ``If you will use SQLServer``
* dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL ``This is for PostgreSQL databases, i use this one``

After all that and having no errors you will use the command `dotnet build` to build the project and if no error is displayed you will do `dotnet run` and your project will be in the `localhost:80`.

Thanks for visiting my repos! You can test it in my online demo on Heroku: https://satisfaction-review.herokuapp.com/
