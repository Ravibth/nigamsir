# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
#ENV ASPNETCORE_URLS=http://*:80

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY RMT.Reports.sln ./
COPY RMT.Reports.Application/*.csproj RMT.Reports.Application/
COPY RMT.Reports.Domain/*.csproj RMT.Reports.Domain/
COPY RMT.Reports.Infrastructure/*.csproj RMT.Reports.Infrastructure/
COPY RMT.Report.Api/*.csproj RMT.Report.Api/
RUN dotnet restore

COPY . .
WORKDIR /src/RMT.Report.Api
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
ENTRYPOINT ["dotnet", "RMT.Report.Api.dll"]
CMD []
