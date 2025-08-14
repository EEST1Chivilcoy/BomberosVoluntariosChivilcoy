# Imagen SDK de .NET 8 para desarrollo local
FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app

# Copiamos todo el repo
COPY . .

# Restauramos dependencias
RUN dotnet restore ./Vista/Vista.sln

WORKDIR /app/Vista

# Exponer puerto para Blazor Server
EXPOSE 5000

# Ejecutar Blazor Server con hot reload
CMD ["dotnet", "watch", "run", "--urls", "http://0.0.0.0:5000"]
