using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using E_Commerce.WEB.Models;
using E_Commerce.WEB.Models.CatalogViewModels;
using E_Commerce.WEB.Models.Pagination;
using E_Commerce.WEB.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_Commerce.WEB.Controllers;

public class CatalogController : Controller
{
    private readonly ILogger<CatalogController> _logger;
    private readonly ICatalogService _catalogService;

    public CatalogController(ILogger<CatalogController> logger, ICatalogService catalogService)
    {
        _logger = logger;
        _catalogService = catalogService;
    }

    public async Task<IActionResult> Index(int? brandFilterApplied, int? typesFilterApplied, int? page,
        [FromQuery] string errorMsg)
    {
        const int itemsPage = 9;
        var catalog = await _catalogService.GetCatalogItems(page ?? 0, itemsPage, brandFilterApplied, typesFilterApplied);

        var vm = new IndexViewModel{

            CatalogItems = catalog.Data,
            Brands = new[]
            {
                new SelectListItem{ Text = "test",Value = "1"}
                    
            },//await _catalogSvc.GetBrands(),
            Types  = new[]
            {
                new SelectListItem{ Text = "test",Value = "1"}
                    
            },//await _catalogSvc.GetTypes(),

            BrandFilterApplied = brandFilterApplied ?? 0,
            TypesFilterApplied = typesFilterApplied ?? 0,
            PaginationInfo = new PaginationInfo
            {
                ActualPage = page ?? 0,
                ItemsPerPage = 20, //catalog.Data.Count,
                TotalItems = 1000, //catalog.Count,
                TotalPages = (int) Math.Ceiling(((decimal) /*catalog.Count*/1000 / itemsPage))
            }
        };

        vm.PaginationInfo.Next = vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1 ? "is-disabled" : "";
        vm.PaginationInfo.Previous = vm.PaginationInfo.ActualPage == 0 ? "is-disabled" : "";

        ViewBag.BasketInoperativeMsg = errorMsg;

        return View(vm);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorVM {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}