# Base para execução em produção (alpine)
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app
EXPOSE 80

# Base para build do projeto (alpine)
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Instalar as dependências do Alpine, incluindo icu-libs para suporte à globalização
RUN apk add --no-cache icu-libs

# Copia apenas os arquivos de projeto para otimizar o cache de build
COPY Training_Now/Training_System/Training_System.csproj Training_System/
COPY Training_Now/Domain/Training_Domain/Training_Domain.csproj Domain/Training_Domain/
COPY Training_Now/Infrastructure/Training_Infrastructure/Training_Infrastructure.csproj Infrastructure/Training_Infrastructure/

# Restaura as dependências
RUN dotnet restore Training_System/Training_System.csproj

# Copia o restante do código-fonte
COPY Training_Now/ .

# Compila o projeto e publica os binários
WORKDIR /src/Training_System
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Imagem final minimalista
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
WORKDIR /app
# Copia somente os binários publicados da etapa anterior
COPY --from=build /app/publish .

# Instala icu-libs para suportar a globalização no runtime
RUN apk add --no-cache icu-libs

# Limpeza de cache do Alpine para reduzir ainda mais o tamanho
RUN rm -rf /var/cache/apk/*

ENTRYPOINT ["dotnet", "Training_System.dll", "--urls", "http://0.0.0.0:80"]
