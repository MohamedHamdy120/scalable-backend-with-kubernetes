#building stage 
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
#copying .csproj first to build fastr with docker caching 
COPY ["UnitsApi.csproj", "./"]
RUN dotnet restore "UnitsApi.csproj"
COPY . .
RUN dotnet publish "UnitsApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

#runtime stage for light weight final container 
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
#copying only the needed file from the building stage
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "UnitsApi.dll"]
