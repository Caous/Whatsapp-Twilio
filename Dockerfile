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
COPY ["src/1 - Presentation/DomainDriveDesign.Presentation.Api/DomainDrivenDesign.Presentation.Api.csproj", "Twilio/"]
COPY ["src/4 - Infrastructure/DomainDrivenDesign.Infrastructure.IoC/DomainDrivenDesign.Infrastructure.IoC.csproj",  "/4 - Infrastructure/DomainDrivenDesign.Infrastructure.IoC/"]
COPY ["src/4 - Infrastructure/DomainDrivenDesign.Infrastructure.Core/DomainDrivenDesign.Infrastructure.Core.csproj",  "/4 - Infrastructure/DomainDrivenDesign.Infrastructure.Core/"]
COPY ["src/4 - Infrastructure/DomainDrivenDesign.Infrastructure.Data/DomainDrivenDesign.Infrastructure.Data.csproj",  "/4 - Infrastructure/DomainDrivenDesign.Infrastructure.Data/"]
COPY ["src/2 - Application/DomainDrivenDesign.Application/DomainDrivenDesign.Application.csproj",  "/2 - Application/DomainDrivenDesign.Application/"]
COPY ["src/3 - Domain/DomainDrivenDesign.Domain/DomainDrivenDesign.Domain.csproj",  "/3 - Domain/DomainDrivenDesign.Domain/"]

# Restaura as dependências
RUN dotnet restore Twilio/DomainDrivenDesign.Presentation.Api.csproj

# Copia o restante do código-fonte
COPY src/ .

# Compila o projeto e publica os binários
WORKDIR /src/Twilio
RUN dotnet publish "DomainDrivenDesign.Presentation.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish

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
