# To Do

This repo is me trying my hand at implementing the clean architecture idea as described by Jason Taylor in this [article](https://jasontaylor.dev/clean-architecture-getting-started/) and this [github repo](https://github.com/jasontaylordev/CleanArchitecture). I recommend reading both as they are excellent resources.

I done this implementation in the context of a to do list app.

## Technologies

- [.NET Core 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [ASP.NET Core 6](https://learn.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-6.0)
- [ASP.NET Core MVC](https://learn.microsoft.com/en-us/aspnet/core/mvc/overview?view=aspnetcore-6.0)
- [Entity Framework Core 7](https://learn.microsoft.com/en-us/ef/core/)
- [AutoMapper](https://automapper.org/)
- [NUnit](https://nunit.org/)
- [Serilog](https://serilog.net/)

## Run Locally

To run locally via Visual Studio, right-click on the solution and select Properties. Then you need to select Multiple startup projects and select `ToDo.API` and `ToDo.Mvc.WebUI` as the startup projects. Then select Start.

![Alt text](image.png)