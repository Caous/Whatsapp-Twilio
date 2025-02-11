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
COPY ["src/1 - Presentation/DomainDriveDesign.Presentation.Api/", "Api/"]
COPY ["src/4 - Infrastructure/DomainDrivenDesign.Infrastructure.IoC/",  "/4 - Infrastructure/DomainDrivenDesign.Infrastructure.IoC/"]
COPY ["src/4 - Infrastructure/DomainDrivenDesign.Infrastructure.Core/",  "/4 - Infrastructure/DomainDrivenDesign.Infrastructure.Core/"]
COPY ["src/4 - Infrastructure/DomainDrivenDesign.Infrastructure.Data/",  "/4 - Infrastructure/DomainDrivenDesign.Infrastructure.Data/"]
COPY ["src/2 - Application/DomainDrivenDesign.Application/",  "/2 - Application/DomainDrivenDesign.Application/"]
COPY ["src/3 - Domain/DomainDrivenDesign.Domain/",  "/3 - Domain/DomainDrivenDesign.Domain/"]

# Restaura as dependências
RUN dotnet restore Api/DomainDrivenDesign.Presentation.Api.csproj

# Copia o restante do código-fonte
COPY /src/ .

# Compila o projeto e publica os binários
WORKDIR /src/Api

RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish 

# Imagem final minimalista
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
WORKDIR /app
# Copia somente os binários publicados da etapa anterior
COPY --from=build /app/publish .

# Instala icu-libs para suportar a globalização no runtime
RUN apk add --no-cache icu-libs

# Limpeza de cache do Alpine para reduzir ainda mais o tamanho
RUN rm -rf /var/cache/apk/*

ENTRYPOINT ["dotnet", "DomainDrivenDesign.Presentation.Api.dll", "--urls", "http://0.0.0.0:80"]
