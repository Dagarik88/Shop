FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY Sho.Core/ShopApi/ShopApi.csproj Shop.Core/Sho.pApi/
RUN dotnet restore Shop.Core/Shop.Api/Shop.Api.csproj
COPY . .
WORKDIR /src/Shop.Core/Shop.Api
RUN dotnet build Shop.Api.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Shop.Api.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Shop.Api.dll"]
