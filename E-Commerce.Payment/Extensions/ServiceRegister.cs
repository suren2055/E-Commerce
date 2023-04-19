using E_Commerce.Payment.Concrete;
using E_Commerce.Payment.Repositories;
using E_Commerce.Payment.Workers;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace E_Commerce.Payment.Extensions;

public static class ServiceRegister
{
    public static void RegisterServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        var connectionString = new NpgsqlConnectionStringBuilder
        {
            Host = "postgres:5432",
            IntegratedSecurity = true,
            Timeout = 60,
            Database = "e-commerce_payment",
            Username = "postgres",
            Password = "postgres"
        };
        services.AddDbContext<EFDBContext>(options =>
            options.UseNpgsql(connectionString.ToString()));


        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddHostedService<OrderingWorker>();
    }
}