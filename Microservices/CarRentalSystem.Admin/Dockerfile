#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CarRentalSystem.Admin/CarRentalSystem.Admin.csproj", "CarRentalSystem.Admin/"]
RUN dotnet restore "CarRentalSystem.Admin/CarRentalSystem.Admin.csproj"
COPY . .
WORKDIR "/src/CarRentalSystem.Admin"
RUN dotnet build "CarRentalSystem.Admin.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarRentalSystem.Admin.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarRentalSystem.Admin.dll"]