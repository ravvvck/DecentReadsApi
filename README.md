## DecentReadsApi
An app created for learning modeled after Goodreads.com.
Frontend can be found [here](https://github.com/ravvvck/DecentReadsFrontend).

## Features
- CRUD operations for books, authors and user entities
- Some operations are restricted by role policy
- Users can give books a review and add them to their favorites list
- Authorization is persisted using refresh token
- Validation for every request

 **To Do:**
- Social functionalities (messages between users, discussion forums)
- Personalized book recommendation 

## Built with:
- [.Net Core 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [AutoMapper](http://automapper.org)
- [Fluent Validation](https://github.com/JeremySkinner/FluentValidation)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/) 
- JWT authentication using [ASP.NET Core JWT Bearer Authentication](https://github.com/aspnet/Security/tree/master/src/Microsoft.AspNetCore.Authentication.JwtBearer).
- [NLog](https://nlog-project.org/)
- Built-in Swagger via [Swashbuckle.AspNetCore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)


