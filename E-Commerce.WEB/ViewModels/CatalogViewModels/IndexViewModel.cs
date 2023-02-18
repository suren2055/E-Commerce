using E_Commerce.WEB.ViewModels.Pagination;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_Commerce.WEB.ViewModels.CatalogViewModels;

public class IndexViewModel
{
    public IEnumerable<CatalogItem> CatalogItems { get; set; }
    public IEnumerable<SelectListItem> Brands { get; set; }
    public IEnumerable<SelectListItem> Types { get; set; }
    public int? BrandFilterApplied { get; set; }
    public int? TypesFilterApplied { get; set; }
    public PaginationInfo PaginationInfo { get; set; }
}