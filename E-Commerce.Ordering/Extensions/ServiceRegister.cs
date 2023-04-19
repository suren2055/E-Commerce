using E_Commerce.Ordering.Concrete;
using E_Commerce.Ordering.Repositories;
using E_Commerce.Ordering.Workers;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace E_Commerce.Ordering.Extensions;

public static class ServiceRegister
{
    public static void RegisterServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        var connectionString = new NpgsqlConnectionStringBuilder
        {
            Host = "postgres:5432",
            IntegratedSecurity = true,
            Timeout = 60,
            Database = "e-commerce_ordering",
            Username = "postgres",
            Password = "postgres"
        };
        services.AddDbContext<EFDBContext>(options =>
            options.UseNpgsql(connectionString.ToString()));

        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddHostedService<PaymentWorker>();
    }
}