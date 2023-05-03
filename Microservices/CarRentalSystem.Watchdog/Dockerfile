FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CarRentalSystem.Watchdog/CarRentalSystem.Watchdog.csproj", "CarRentalSystem.Watchdog/"]
COPY ["CarRentalSystem/CarRentalSystem.csproj", "CarRentalSystem/"]
RUN dotnet restore "CarRentalSystem.Watchdog/CarRentalSystem.Watchdog.csproj"
COPY . .
WORKDIR "/src/CarRentalSystem.Watchdog"
RUN dotnet build "CarRentalSystem.Watchdog.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarRentalSystem.Watchdog.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarRentalSystem.Watchdog.dll"]