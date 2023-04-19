FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["E-Commerce.Payment/E-Commerce.Payment.csproj", "E-Commerce.Payment/"]
RUN dotnet restore "E-Commerce.Payment/E-Commerce.Payment.csproj"
COPY . .
WORKDIR "/src/E-Commerce.Payment"
RUN dotnet build "E-Commerce.Payment.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "E-Commerce.Payment.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "E-Commerce.Payment.dll"]
