# Etapa 1: Imagem base para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copiar o arquivo de projeto e restaurar as dependências
COPY *.csproj ./
RUN dotnet restore

# Copiar o restante do código e fazer o build
COPY . ./
RUN dotnet publish -c Release -o out

# Etapa 2: Imagem base para runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Definir o comando de inicialização
CMD ASPNETCORE_URLS="http://*:$PORT" dotnet Sistema_Web_Gestao.dll
