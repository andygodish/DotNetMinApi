# .NET6 Minimal Commands API

This readme will walk you through the steps of building a .NET minimal API. The end result will be a Min API with a single model used to store CLI commands for various tools (dotnet, k8s, docker, etc).

- [Reference Video](https://www.youtube.com/watch?v=5YB49OEmbbE&t=926s)

The purpose of this repository is to breakdown the components that go into building and packaging (docker) a service built with .NET6. We'll be using the minimal API framework from .NET6. There are differnces between the minimal APIs and the traditional MVC framework offered by .NET6; see the ending of the reference video for a detailed explanation of those differences. 

## Spinning Up a Dev Database

### Prerequisites
- docker + docker-compose
- Micro

Utilize the `docker-compose.yaml` in the root directory of this repository to launch an instance of MSSQL. The stop command can be used to terminate the instance.

```
docker-compose up -d

docker-compse stop
```

I also use MS SQL Management Server as a GUI for insight into the instance. Use the user, `sa`, and the `sa_password` defined as an env variable in the docker-compose yaml file to sign in to the instance running on `localhost`. 

Additionally, you can use netcat (nc) to determine a valid connection to a server:port, `nc -zv localhost 1433`.

>I use WSL (Ubuntu) on my local Windows machine. When I launch this container using docker in my Ubuntu environment and try to connect MSQLMS to it, the localhost address does not work. I am not sure what IP address I need to use to connect to this from the Windows environment itself.

Subsequent steps will use this database following the migration of the schema defined by the application code. Leave it running for now. 

## Aplication Scaffolding

Use the dotnet cli to bootstrap a new project that utilizes the `webapi` template and sepcifies that it should be a minimal api with the `-minimal` flag. This will result in a base project that contains the following important files: 
- appsettings.json 
- program.cs
- \<project\>.csproj

Deploy locally by running `dotnet run` and note that by default your API is served on port 7126. 
- I believe the port can be configured in your appsettings.json file
    - when using `dotnet run`, is the appsettings.Development.json file being used for configuration?

## Application Components

### External Packages

These can be imported via the dotnet cli and viewed in the `.csproj` file. These can be imported by your application packages with the `using` declaration at the top of a .cs file. For example, to add a package, run: `dotnet add package Microsoft.EntityFrameworkCore`, then view it in the `<ItemGroup>` block of your `.csproj` file. 

```
<ItemGroup>
    <PackageReference Include="AutoMapper.Extensions.Microsoft.DependencyInjection" Version="11.0.0" />
    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="6.0.8" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="6.0.8">
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
      <PrivateAssets>all</PrivateAssets>
    </PackageReference>
    <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="6.0.8" />
    <PackageReference Include="Swashbuckle.AspNetCore" Version="6.2.3" />
  </ItemGroup>
```



