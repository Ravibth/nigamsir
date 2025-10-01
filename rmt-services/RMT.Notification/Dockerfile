# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
#ENV ASPNETCORE_URLS=http://*:80

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY RMT.Notification.sln ./
COPY RMT.Notification.Application/*.csproj RMT.Notification.Application/
COPY RMT.Notification.Domain/*.csproj RMT.Notification.Domain/
COPY RMT.Notification.Infrastructure/*.csproj RMT.Notification.Infrastructure/
COPY RMT.Notification.API/*.csproj RMT.Notification.API/
RUN dotnet restore

COPY . .
WORKDIR /src/RMT.Notification.API
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
ENTRYPOINT ["dotnet", "RMT.Notification.API.dll"]
CMD []
