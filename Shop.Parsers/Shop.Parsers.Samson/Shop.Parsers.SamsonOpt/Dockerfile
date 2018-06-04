FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY Shop.Parsers/Shop.Parsers.Samson/Shop.Parsers.SamsonOpt/Shop.Parsers.SamsonOpt.csproj Shop.Parsers/Shop.Parsers.Samson/Shop.Parsers.SamsonOpt/
RUN dotnet restore Shop.Parsers/Shop.Parsers.Samson/Shop.Parsers.SamsonOpt/Shop.Parsers.SamsonOpt.csproj
COPY . .
WORKDIR /src/Shop.Parsers/Shop.ParsersSamson/ShopParsersSamsonOpt
RUN dotnet build Shop.Parsers.SamsonOpt.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Shop.Parsers.SamsonOpt.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Shop.Parsers.SamsonOpt.dll"]
