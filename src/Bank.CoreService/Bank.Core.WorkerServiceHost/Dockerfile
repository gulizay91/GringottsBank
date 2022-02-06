#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT=Development
ENV NSBTransportHost=rabbitmq
ENV MssqlConnectionString=Server=sql-data;Database=GringottsBankDb;User=sa;Password=p@ssw0rd

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Bank.CoreService/Bank.Core.WorkerServiceHost/Bank.Core.WorkerServiceHost.csproj", "src/Bank.CoreService/Bank.Core.WorkerServiceHost/"]
COPY ["src/Bank.CoreService/Bank.Core.Infrastructure/Bank.Core.Infrastructure.csproj", "src/Bank.CoreService/Bank.Core.Infrastructure/"]
COPY ["src/Bank.CoreService/Bank.Core/Bank.Core.csproj", "src/Bank.CoreService/Bank.Core/"]
COPY ["src/Bank.CoreService/Bank.Core.Contract/Bank.Core.Contract.csproj", "src/Bank.CoreService/Bank.Core.Contract/"]
COPY ["src/Bank.Shared/Bank.Shared.csproj", "src/Bank.Shared/"]
RUN dotnet restore "src/Bank.CoreService/Bank.Core.WorkerServiceHost/Bank.Core.WorkerServiceHost.csproj"
COPY . .
WORKDIR "/src/src/Bank.CoreService/Bank.Core.WorkerServiceHost"
RUN dotnet build "Bank.Core.WorkerServiceHost.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Bank.Core.WorkerServiceHost.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bank.Core.WorkerServiceHost.dll"]