using System.Text.Json;
using E_Commerce.WEB.Helpers;
using E_Commerce.WEB.ViewModels;
using E_Commerce.WEB.Services.Models.DTO;
using Microsoft.Extensions.Options;

namespace E_Commerce.WEB.Services;

public class BasketService : IBasketService
{
    private readonly IOptions<AppSettings> _settings;
    private readonly ILogger<BasketService> _logger;
    private readonly string _basketByPassUrl;
    private readonly string _purchaseUrl;

    public BasketService(ILogger<BasketService> logger, IOptions<AppSettings> settings)
    {
        _settings = settings;
        _logger = logger;
        _basketByPassUrl = $"{_settings.Value.RecourceUrl}/Basket";
    }

    public async Task<Basket> GetBasket(ApplicationUser user)
    {
        var responseString = await HttpCaller.SendAsync(new HttpRequestInput
        {
            Methods = HttpMethod.Get,
            Url = _basketByPassUrl + "/8bf8d740-b087-4171-9fca-efdae425a034"
        });
        var basket = JsonSerializer.Deserialize<Basket>(responseString.Response,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        return basket;
    }

    public async Task<Basket> UpdateBasket(Basket basket)
    {
        return null;
    }
    
    public async Task Checkout(BasketDTO basket)
    {
    }

    public async Task<Basket> SetQuantities(ApplicationUser user, Dictionary<string, int> quantities)
    {
        return null;
    }

    public async Task<Order> GetOrderDraft(string basketId)
    {
        return null;
    }

    public async Task AddItemToBasket(ApplicationUser user, CatalogItem item)
    {


        var basketItem = new BasketItem
        {
            Id = Guid.NewGuid().ToString(),
            Quantity = 1,
            PictureUrl = item.PictureUri,
            ProductId = item.Id,
            ProductName = item.Name,
            UnitPrice = item.Price,
            OldUnitPrice = 300
        };
        var basket = GetBasket(user).Result;
        basket.Items.Add(basketItem);
        
        var responseString = await HttpCaller.SendAsync(new HttpRequestInput
        {
            Methods = HttpMethod.Post,
            Url = _basketByPassUrl + "/UpdateBasket",
            Data = JsonSerializer.Serialize(basket),
            ContentType = "application/json"
        });
    }
}