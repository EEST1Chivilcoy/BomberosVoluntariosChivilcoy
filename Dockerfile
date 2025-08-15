# Etapa base para desarrollo con SDK completo
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS dev
WORKDIR /app

# Copiar solo archivos de proyecto y solución para cachear dependencias
COPY Vista/*.csproj ./Vista/
COPY BlazorApp1.sln ./
RUN dotnet restore ./Vista/Vista.csproj

# Copiar el resto del código
COPY . .

# Ir al directorio del proyecto
WORKDIR /app/Vista

# Exponer puerto para Blazor Server
EXPOSE 5000

# Hot reload en desarrollo
CMD ["dotnet", "watch", "run", "--urls", "http://0.0.0.0:5000"]