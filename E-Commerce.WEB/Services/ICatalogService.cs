using E_Commerce.WEB.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_Commerce.WEB.Services;
public interface ICatalogService
{
    Task<Catalog> GetCatalogItems(int page, int take, int? brand, int? type);
    Task<IEnumerable<SelectListItem>> GetBrands();
    Task<IEnumerable<SelectListItem>> GetTypes();
}