FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["E-Commerce.WEB/E-Commerce.WEB.csproj", "E-Commerce.WEB/"]
RUN dotnet restore "E-Commerce.WEB/E-Commerce.WEB.csproj"
COPY . .
WORKDIR "/src/E-Commerce.WEB"
RUN dotnet build "E-Commerce.WEB.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "E-Commerce.WEB.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "E-Commerce.WEB.dll"]
