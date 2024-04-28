#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Test/UnitTest/UnitTest.csproj", "src/Test/UnitTest/"]
COPY ["src/CacheLibraries/Hasin.InMemory/Hasin.InMemory.csproj", "src/CacheLibraries/Hasin.InMemory/"]
COPY ["src/CacheLibraries/Hasin.CacheCore/Hasin.CacheCore.csproj", "src/CacheLibraries/Hasin.CacheCore/"]
COPY ["src/Core/Domain/Domain.csproj", "src/Core/Domain/"]
COPY ["src/CacheLibraries/Hasin.Redis/Hasin.Redis.csproj", "src/CacheLibraries/Hasin.Redis/"]
RUN dotnet restore "./src/Test/UnitTest/./UnitTest.csproj"
COPY . .
WORKDIR "/src/src/Test/UnitTest"
RUN dotnet build "./UnitTest.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./UnitTest.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UnitTest.dll"]