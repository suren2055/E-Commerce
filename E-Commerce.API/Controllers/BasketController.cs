using System.Net;
using E_Commerce.API.Models.DTO;
using E_Commerce.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BasketController : ControllerBase
{
    private readonly IBasketRepository _repository;
    private readonly ILogger<BasketController> _logger;

    public BasketController(ILogger<BasketController> logger, IBasketRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CustomerBasketDTO), (int) HttpStatusCode.OK)]
    public async Task<ActionResult<CustomerBasketDTO>> GetBasketByIdAsync(Guid id)
    {
        var basket = await _repository.GetBasketAsync(id);

        return Ok(basket ?? new CustomerBasketDTO(id));
    }

    [HttpPost("UpdateBasket")]
    public async Task<ActionResult<CustomerBasketDTO>> UpdateBasket([FromBody] CustomerBasketDTO value)
    {
        return Ok(await _repository.UpdateBasketAsync(value));
    }
    
    
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(void), (int) HttpStatusCode.OK)]
    public async Task DeleteBasketByIdAsync(Guid id)
    {
        await _repository.DeleteBasketAsync(id);
    }
}