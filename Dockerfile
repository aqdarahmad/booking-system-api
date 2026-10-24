
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

<<<<<<< HEAD

ENTRYPOINT ["dotnet", "Booking-system-API.dll"]
=======
ENTRYPOINT ["dotnet", "Booking-System-API.dll"]
>>>>>>> edd27e9fb5311b6a6e98a46e9d3041c793805b2c
