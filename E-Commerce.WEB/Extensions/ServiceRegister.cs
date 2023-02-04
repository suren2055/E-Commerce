using E_Commerce.WEB.Models;
using E_Commerce.WEB.Services;

namespace E_Commerce.WEB.Extensions;

public  static class ServiceRegister
{
    public static void RegisterServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.Configure<AppSettings>(configuration.GetSection("Settings"));
        services.AddScoped<ICatalogService, CatalogService>();
    }
}