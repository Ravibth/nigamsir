# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
#ENV ASPNETCORE_URLS=http://*:80

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY RMT.Projects.sln ./
COPY RMT.Projects.Application/*.csproj RMT.Projects.Application/
COPY RMT.Projects.Domain/*.csproj RMT.Projects.Domain/
COPY RMT.Projects.Infrastructure/*.csproj RMT.Projects.Infrastructure/
COPY RMT.Projects.API/*.csproj RMT.Projects.API/
RUN dotnet restore

COPY . .
WORKDIR /src/RMT.Projects.API
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
ENTRYPOINT ["dotnet", "RMT.Projects.API.dll"]
CMD []
