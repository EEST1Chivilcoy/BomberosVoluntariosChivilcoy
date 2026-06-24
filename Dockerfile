# Etapa base para desarrollo con SDK completo
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS dev
WORKDIR /app

# Copiar solo la solución y los .csproj para cachear el restore de dependencias
COPY FireForce.sln ./
COPY FireForce.Core/*.csproj ./FireForce.Core/
COPY FireForce.Client/*.csproj ./FireForce.Client/
COPY FireForce.Data/*.csproj ./FireForce.Data/
COPY FireForce.Shared/*.csproj ./FireForce.Shared/
RUN dotnet restore ./FireForce.Core/FireForce.Core.csproj

# Copiar el resto del código
COPY . .

# Ir al directorio del proyecto host (startup project)
WORKDIR /app/FireForce.Core

# Exponer puerto para Blazor Server
EXPOSE 5000

# Hot reload en desarrollo
CMD ["dotnet", "watch", "run", "--urls", "http://0.0.0.0:5000"]
