## DecentReadsApi
An app created for learning modeled after Goodreads.com.
Frontend can be found [here](https://github.com/ravvvck/DecentReadsFrontend).

## Features
- CRUD operations for books, authors and user entities
- Authorization is persisted using JWT and refresh tokens
- Validation for every request
- Global error handling middleware

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

## Docker
You can start this project with the command
```console
docker-compose up
```
The application runs at `http://localhost:8080/swagger`

![swagger](https://user-images.githubusercontent.com/73526574/202506785-faa42acc-2f37-4c7c-80d8-ccdd5de2e598.png)



