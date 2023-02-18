using E_Commerce.API.Models.DTO;
using E_Commerce.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CatalogController : ControllerBase
{
    private readonly ILogger<CatalogController> _logger;
    private readonly ICatalogItemRepository _catalogItemRepository;

    public CatalogController(ILogger<CatalogController> logger, ICatalogItemRepository catalogItemRepository)
    {
        _logger = logger;
        _catalogItemRepository = catalogItemRepository;
    }

    [HttpGet("Get")]
    public async Task<IActionResult> Get()
    {
        var data = await _catalogItemRepository.GetAsync();
        var catalog = new CatalogDTO
        {
            Count = 10,
            Data = data.Select(x => new CatalogItemDTO
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                PictureUri = x.PictureUri,
                CatalogBrandId = x.CatalogBrandId,
                CatalogBrand = x.CatalogBrand,
                CatalogTypeId = x.CatalogTypeId,
                CatalogType = x.CatalogType
            }).ToList(),
            PageIndex = 0,
            PageSize = 1000
        };
        return Ok(catalog);
    }
    
    
}