using E_Commerce.API.Concrete;
using E_Commerce.API.Models;
using E_Commerce.API.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Npgsql;
using StackExchange.Redis;

namespace E_Commerce.API.Extensions;

internal static class ServiceRegister
{
    public static void RegisterServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.Configure<AppSettings>(configuration.GetSection("Settings"));
        var connectionString = new NpgsqlConnectionStringBuilder
        {
            Host = "postgres:5432",//Environment.GetEnvironmentVariable("DB_ADDR"),
            IntegratedSecurity = true,
            Timeout = 60,
            Database = "e-commerce",//Environment.GetEnvironmentVariable("DB_DATABASE"),
            Username = "postgres",//Environment.GetEnvironmentVariable("DB_USER"),
            Password = "postgres"//Environment.GetEnvironmentVariable("DB_PASSWORD")
        };
        services.AddDbContext<EFDBContext>(options =>
            options.UseNpgsql(connectionString.ToString()));
        services.AddScoped<IBasketRepository, RedisBasketRepository>();
        services.AddSingleton<ConnectionMultiplexer>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<AppSettings>>().Value;
            var configuration = ConfigurationOptions.Parse(settings.RedisConnectionString, true);
            configuration.AbortOnConnectFail = false;
            return ConnectionMultiplexer.Connect(configuration);
        });
        services.AddScoped<ICatalogItemRepository, CatalogItemRepository>();
    }
}