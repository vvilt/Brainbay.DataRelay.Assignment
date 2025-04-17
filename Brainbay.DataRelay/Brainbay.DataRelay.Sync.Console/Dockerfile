# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Brainbay.DataRelay.Sync.Console/Brainbay.DataRelay.Sync.Console.csproj", "Brainbay.DataRelay.Sync.Console/"]
COPY ["Brainbay.DataRelay.DataAccess.SQL/Brainbay.DataRelay.DataAccess.SQL.csproj", "Brainbay.DataRelay.DataAccess.SQL/"]
COPY ["Brainbay.DataRelay.Domain/Brainbay.DataRelay.Domain.csproj", "Brainbay.DataRelay.Domain/"]
COPY ["Brainbay.DataRelay.Sync/Brainbay.DataRelay.Sync.csproj", "Brainbay.DataRelay.Sync/"]
RUN dotnet restore "./Brainbay.DataRelay.Sync.Console/Brainbay.DataRelay.Sync.Console.csproj"
COPY . .
WORKDIR "/src/Brainbay.DataRelay.Sync.Console"
RUN dotnet build "./Brainbay.DataRelay.Sync.Console.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Brainbay.DataRelay.Sync.Console.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Brainbay.DataRelay.Sync.Console.dll"]