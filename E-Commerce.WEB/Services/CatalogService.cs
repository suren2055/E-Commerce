using System.Text.Json;
using E_Commerce.WEB.Helpers;
using E_Commerce.WEB.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;

namespace E_Commerce.WEB.Services;

public class CatalogService : ICatalogService
{
    private readonly IOptions<AppSettings> _settings;
    
    private readonly ILogger<CatalogService> _logger;

    private readonly string _remoteServiceBaseUrl;

    public CatalogService(ILogger<CatalogService> logger, IOptions<AppSettings> settings)
    {
        _settings = settings;
        _logger = logger;
       
        _remoteServiceBaseUrl = $"{_settings.Value.RecourceUrl}/Catalog/Get";
    }

    public async Task<Catalog> GetCatalogItems(int page, int take, int? brand, int? type)
    {
        

        var responseString = await HttpCaller.SendAsync(new HttpRequestInput
        {
            Methods = HttpMethod.Get,
            Url = _remoteServiceBaseUrl
        });


        var catalog = JsonSerializer.Deserialize<Catalog>(responseString.Response,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        return catalog;
    }

    public async Task<IEnumerable<SelectListItem>> GetBrands()
    {
        return null;
    }

    public async Task<IEnumerable<SelectListItem>> GetTypes()
    {
        return null;
    }
}