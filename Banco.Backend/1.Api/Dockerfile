#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["1.Api/Banco.Api.csproj", "1.Api/"]
COPY ["4.Infra/4.2.CrossCutting/Banco.Infra.CrossCutting.IoC/Banco.Infra.CrossCutting.IoC.csproj", "4.Infra/4.2.CrossCutting/Banco.Infra.CrossCutting.IoC/"]
COPY ["2.Service/Banco.Service.Contract/Banco.Service.Contract.csproj", "2.Service/Banco.Service.Contract/"]
COPY ["3.Domain/Banco.Backend.Domain.Contract/Banco.Domain.Contract.csproj", "3.Domain/Banco.Backend.Domain.Contract/"]
COPY ["2.Service/Banco.Service/Banco.Service.csproj", "2.Service/Banco.Service/"]
COPY ["3.Domain/Banco.Domain/Banco.Domain.csproj", "3.Domain/Banco.Domain/"]
COPY ["4.Infra/Banco.Infra.Data.Contract/Banco.Infra.Data.Contract.csproj", "4.Infra/Banco.Infra.Data.Contract/"]
COPY ["4.Infra/4.1.Data/Banco.Infra.Data/Banco.Infra.Data.csproj", "4.Infra/4.1.Data/Banco.Infra.Data/"]
RUN dotnet restore "1.Api/Banco.Api.csproj"
COPY . .
WORKDIR "/src/1.Api"
RUN dotnet build "Banco.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Banco.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Banco.Api.dll"]