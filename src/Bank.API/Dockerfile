#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT=Development
ENV NSBTransportHost=rabbitmq
ENV MssqlConnectionString=Server=sql-data;Database=GringottsBankDb;User=sa;Password=p@ssw0rd

EXPOSE 80
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Bank.API/Bank.API.csproj", "src/Bank.API/"]
COPY ["src/Bank.CoreService/Bank.Core.Contract/Bank.Core.Contract.csproj", "src/Bank.CoreService/Bank.Core.Contract/"]
COPY ["src/Bank.Shared/Bank.Shared.csproj", "src/Bank.Shared/"]
COPY ["src/Bank.CoreService/Bank.Core.Infrastructure/Bank.Core.Infrastructure.csproj", "src/Bank.CoreService/Bank.Core.Infrastructure/"]
COPY ["src/Bank.CoreService/Bank.Core/Bank.Core.csproj", "src/Bank.CoreService/Bank.Core/"]
RUN dotnet restore "src/Bank.API/Bank.API.csproj"
COPY . .
WORKDIR "/src/src/Bank.API"
RUN dotnet build "Bank.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Bank.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bank.API.dll"]