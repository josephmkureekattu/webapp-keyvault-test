#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/azure-functions/dotnet:4 AS base
WORKDIR /home/site/wwwroot
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FunctionApp3/FunctionApp3.csproj", "FunctionApp3/"]
COPY ["Persistence/Persistence.csproj", "Persistence/"]
COPY ["Core/Core.csproj", "Core/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
RUN dotnet restore "FunctionApp3/FunctionApp3.csproj"
COPY . .
WORKDIR "/src/FunctionApp3"
RUN dotnet build "FunctionApp3.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FunctionApp3.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /home/site/wwwroot
COPY --from=publish /app/publish .
ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    AzureFunctionsJobHost__Logging__Console__IsEnabled=true