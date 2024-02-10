#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 6969

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Prolog.Api/Prolog.Api.csproj", "Prolog.Api/"]
COPY ["Prolog.Abstractions/Prolog.Abstractions.csproj", "Prolog.Abstractions/"]
COPY ["Prolog.Core/Prolog.Core.csproj", "Prolog.Core/"]
COPY ["Prolog.Domain/Prolog.Domain.csproj", "Prolog.Domain/"]
COPY ["Prolog.Application/Prolog.Application.csproj", "Prolog.Application/"]
COPY ["Prolog.Infrastructure/Prolog.Infrastructure.csproj", "Prolog.Infrastructure/"]
COPY ["Prolog.Keycloak/Prolog.Keycloak.csproj", "Prolog.Keycloak/"]
COPY ["Prolog.Services/Prolog.Services.csproj", "Prolog.Services/"]
RUN dotnet restore "./Prolog.Api/./Prolog.Api.csproj"
COPY . .
WORKDIR "/src/Prolog.Api"
RUN dotnet build "./Prolog.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Prolog.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Prolog.Api.dll"]