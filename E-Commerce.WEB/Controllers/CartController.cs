using E_Commerce.WEB.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.WEB.Controllers;

public class CartController : Controller
{
   
    public IActionResult Index()
    {
        return null;
    }

    public async Task<IActionResult> AddToCart(CatalogItem productDetails)
    {
        return null;
        // try
        // {
        //     if (productDetails?.Id != null)
        //     {
        //         var user = _appUserParser.Parse(HttpContext.User);
        //         await _basketSvc.AddItemToBasket(user, productDetails.Id);
        //     }
        //     return RedirectToAction("Index", "Catalog");
        // }
        // catch (Exception ex)
        // {
        //     // Catch error when Basket.api is in circuit-opened mode                 
        //     HandleException(ex);
        // }
        //
        // return RedirectToAction("Index", "Catalog", new { errorMsg = ViewBag.BasketInoperativeMsg });
    }
}