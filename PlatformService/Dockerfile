FROM mcr.microsoft.com/dotnet/aspnet:8.0 as base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /src
COPY ["PlatformService.csproj", "."]
RUN dotnet restore "PlatformService.csproj"
COPY . .

FROM build AS publish
RUN dotnet publish "PlatformService.csproj" -c Release -o /app/publish

FROM base AS final
COPY --from=publish /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "PlatformService.dll"]