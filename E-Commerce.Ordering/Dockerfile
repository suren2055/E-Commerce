FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["E-Commerce.Ordering/E-Commerce.Ordering.csproj", "E-Commerce.Ordering/"]
RUN dotnet restore "E-Commerce.Ordering/E-Commerce.Ordering.csproj"
COPY . .
WORKDIR "/src/E-Commerce.Ordering"
RUN dotnet build "E-Commerce.Ordering.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "E-Commerce.Ordering.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "E-Commerce.Ordering.dll"]
