FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["CommandService.csproj", "."]
RUN dotnet restore "CommandService.csproj"
COPY . .

FROM build AS publish
RUN dotnet publish "CommandService.csproj" -c Release -o /app/publish

FROM base AS final
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "CommandService.dll"]