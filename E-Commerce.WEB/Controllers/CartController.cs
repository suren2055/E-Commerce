using E_Commerce.WEB.ViewModels;
using E_Commerce.WEB.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.WEB.Controllers;

public class CartController : Controller
{
    private readonly IBasketService _basketSvc;
    private readonly ICatalogService _catalogSvc;

    public CartController(IBasketService basketSvc, ICatalogService catalogSvc)
    {
        _basketSvc = basketSvc;
        _catalogSvc = catalogSvc;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var user = new ApplicationUser {Id = "5", Name = "John", LastName = "Doe"};
            var vm = await _basketSvc.GetBasket(user);

            return View(vm);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return View();
    }

    public async Task<IActionResult> AddToCart(CatalogItem productDetails)
    {
        
        try
        {
            if (productDetails?.Id == null) 
                return RedirectToAction("Index", "Catalog");
            var user = new ApplicationUser
            {
                Id = "5",
                Name = "John",
                LastName = "Doe"
            };
            var item =  _catalogSvc.GetCatalogItems(0, 9, null, null)
                .Result.Data.FirstOrDefault(x => x.Id==productDetails.Id);
            await _basketSvc.AddItemToBasket(user, item);
            return RedirectToAction("Index", "Catalog");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        
        return RedirectToAction("Index", "Catalog", new { errorMsg = ViewBag.BasketInoperativeMsg });
    }
}