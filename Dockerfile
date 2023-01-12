FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

WORKDIR /app

COPY *.csproj ./

# pull down packages we need to build our image 
RUN dotnet restore

COPY . ./ 

# outputs to a dir called "out" using the Release configuration
RUN dotnet publish -c Release -o out 

FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /app

COPY --from=build-env /app/out .

EXPOSE 80

ENTRYPOINT ["dotnet", "DotnetMinApi.dll"]