using System.Net;
using E_Commerce.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(ILogger<OrdersController> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
}