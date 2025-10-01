# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
#ENV ASPNETCORE_URLS=http://*:80

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY RMT.Allocations.API.sln ./
COPY RMT.Allocation.Application/*.csproj RMT.Allocation.Application/
COPY RMT.Allocation.Domain/*.csproj RMT.Allocation.Domain/
COPY RMT.Allocation.Infrastructure/*.csproj RMT.Allocation.Infrastructure/
COPY RMT.Allocations/*.csproj RMT.Allocations/
RUN dotnet restore

COPY . .
WORKDIR /src/RMT.Allocations
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
ENTRYPOINT ["dotnet", "RMT.Allocations.API.dll"]
CMD []
