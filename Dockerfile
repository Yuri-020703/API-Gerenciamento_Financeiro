# imagem oficial do .NET 9 SDK para build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copiar csproj e restaurar dependências
COPY *.csproj ./
RUN dotnet restore

# Copiar todo o código e publicar a aplicação
COPY . ./
RUN dotnet publish Gerenciamento_Financeiro.csproj -c Release -o out

# Criar imagem final
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Expor porta
EXPOSE 5000

# Comando para rodar a API
ENTRYPOINT ["dotnet", "Gerenciamento_Financeiro.csproj"]
