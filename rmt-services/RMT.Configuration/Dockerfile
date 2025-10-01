# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
#ENV ASPNETCORE_URLS=http://*:80

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY RMT.Configuration.sln ./
COPY RMT.Configuration.Application/*.csproj RMT.Configuration.Application/
COPY RMT.Configuration.Domain/*.csproj RMT.Configuration.Domain/
COPY RMT.Configuration.Infrastructure/*.csproj RMT.Configuration.Infrastructure/
COPY RMT.Configuration/*.csproj RMT.Configuration/
RUN dotnet restore

COPY . .
WORKDIR /src/RMT.Configuration
ARG ARG_BuildProfile=Release
RUN dotnet build -c ${ARG_BuildProfile} -o /app

# Publish stage
FROM build AS publish
ARG ARG_BuildProfile=Release
RUN dotnet publish -c ${ARG_BuildProfile} --no-restore -o /app

# Final runtime stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "RMT.Configuration.API.dll"]
CMD []
