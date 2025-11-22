# Build Aþamasý
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["UserRegisterApp.csproj", "."]
RUN dotnet restore "UserRegisterApp.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet publish "UserRegisterApp.csproj" -c Release -o /app/publish

# Çalýþtýrma Aþamasý
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "UserRegisterApp.dll"]