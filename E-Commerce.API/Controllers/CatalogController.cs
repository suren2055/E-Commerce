using E_Commerce.API.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CatalogController : ControllerBase
{
    
    private readonly ILogger<CatalogController> _logger;

    public CatalogController(ILogger<CatalogController> logger)
    {
        _logger = logger;
    }

    [HttpGet("Get")]
    public IActionResult Get()
    {
        var catalogItems = new []
        {
            new CatalogItemDTO
            {
                Id = 1,
                Name = "Cherry",
                Description = "Test Description",
                Price = 56444,
                PictureUri = "/images/fe545665-6d7a-429d-8b15-2de83b870499/cherry.jpg",
                CatalogBrandId = 1,
                CatalogBrand = "Test Brand",
                CatalogTypeId = 1,
                CatalogType = "Test catalog type"
            },
            new CatalogItemDTO
            {
                Id = 2,
                Name = "Peatch",
                Description = "Test Description",
                Price = 3450,
                PictureUri = "/images/dbdd7cb0-7970-4160-a3b4-aa2da308bb69/peatch.jpg",
                CatalogBrandId = 1,
                CatalogBrand = "Test Brand",
                CatalogTypeId = 1,
                CatalogType = "Test catalog type"
            }
        };
        var catalog = new CatalogDTO
        {
            Count = 10,
            Data = catalogItems.ToList(),
            PageIndex = 0,
            PageSize = 1000

        };
        return Ok(catalog);
    }
}