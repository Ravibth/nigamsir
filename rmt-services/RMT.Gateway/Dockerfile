# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
#ENV ASPNETCORE_URLS=http://*:80

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY Gateway.sln ./
COPY Gateway.API/*.csproj Gateway.API/
RUN dotnet restore

COPY . .
WORKDIR /src/Gateway.API
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
ENTRYPOINT ["dotnet", "Gateway.API.dll"]
CMD []
