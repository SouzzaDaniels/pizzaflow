# ----- Build -----
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY PizzaFlow.Api.csproj ./
RUN dotnet restore "PizzaFlow.Api.csproj"

COPY . .
RUN dotnet publish "PizzaFlow.Api.csproj" -c Release -o /app/publish --no-restore

# ----- Runtime -----
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# O Render define a variável PORT dinamicamente em tempo de execução;
# o Program.cs lê essa variável para configurar a URL do Kestrel.
EXPOSE 8080

ENTRYPOINT ["dotnet", "PizzaFlow.Api.dll"]
