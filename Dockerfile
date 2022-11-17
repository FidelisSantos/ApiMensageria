FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build 
WORKDIR /source
COPY . .
RUN dotnet restore "ApiMensageria.csproj" --disable-parallel
RUN dotnet publish "ApiMensageria.csproj" -c realease -o /app --no-restore

FROM  mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5000

ENTRYPOINT ["dotnet", "ApiMensageria.dll"]