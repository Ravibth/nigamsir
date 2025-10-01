# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
#ENV ASPNETCORE_URLS=http://*:80

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY RMT.Skill.sln ./
COPY RMT.Skill.Application/*.csproj RMT.Skill.Application/
COPY RMT.Skill.Domain/*.csproj RMT.Skill.Domain/
COPY RMT.Skill.Infrastructure/*.csproj RMT.Skill.Infrastructure/
COPY RMT.Skill.API/*.csproj RMT.Skill.API/
RUN dotnet restore

COPY . .
WORKDIR /src/RMT.Skill.API
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
ENTRYPOINT ["dotnet", "RMT.Skill.API.dll"]
CMD []
