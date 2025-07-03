# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar el archivo del proyecto primero
COPY MicroservicioDemo2.csproj ./

# Restaurar dependencias
RUN dotnet restore

# Copiar el resto del código
COPY . .

# Compilar
RUN dotnet build MicroservicioDemo2.csproj -c Release -o /app/build

# Etapa 2: Publish
FROM build AS publish
RUN dotnet publish MicroservicioDemo2.csproj -c Release -o /app/publish

# Etapa 3: Imagen final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroservicioDemo2.dll"]