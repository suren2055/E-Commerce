using E_Commerce.WEB.ViewModels;
using E_Commerce.WEB.Services.Models.DTO;

namespace E_Commerce.WEB.Services;

public interface IBasketService
{
    Task<Basket> GetBasket(ApplicationUser user);
    Task AddItemToBasket(ApplicationUser user, CatalogItem item);
    Task<Basket> UpdateBasket(Basket basket);
    Task Checkout(BasketDTO basket);
    Task<Basket> SetQuantities(ApplicationUser user, Dictionary<string, int> quantities);
    Task<Order> GetOrderDraft(string basketId);
}