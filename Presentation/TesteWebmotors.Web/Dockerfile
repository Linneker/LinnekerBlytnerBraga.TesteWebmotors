#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TesteWebmotors.Web/TesteWebmotors.Web.csproj", "TesteWebmotors.Web/"]
RUN dotnet restore "TesteWebmotors.Web/TesteWebmotors.Web.csproj"
COPY . .
WORKDIR "/src/TesteWebmotors.Web"
RUN dotnet build "TesteWebmotors.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TesteWebmotors.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TesteWebmotors.Web.dll"]