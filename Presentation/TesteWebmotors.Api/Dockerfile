#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Presentation/TesteWebmotors.Api/TesteWebmotors.Api.csproj", "Presentation/TesteWebmotors.Api/"]
RUN dotnet restore "Presentation/TesteWebmotors.Api/TesteWebmotors.Api.csproj"
COPY . .
WORKDIR "/src/Presentation/TesteWebmotors.Api"
RUN dotnet build "TesteWebmotors.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TesteWebmotors.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TesteWebmotors.Api.dll"]