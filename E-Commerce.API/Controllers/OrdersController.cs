using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using E_Commerce.API.Entities;
using E_Commerce.API.Models.DTO;
using E_Commerce.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Order = StackExchange.Redis.Order;

namespace E_Commerce.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly ILogger<OrdersController> _logger;
    private readonly IOrderRepository _orderRepository;

    public OrdersController(ILogger<OrdersController> logger, IOrderRepository orderRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _orderRepository = orderRepository;
    }

    [HttpGet("Get")]
    public async Task<IActionResult> Get([FromQuery] string? id)
    {
        

        //Further we can use auto mapper
        var all = _orderRepository.GetAsync().Result.Select(x => new OrderDTO
        {
            OrderId = x.OrderId,
            OrderDate = x.OrderDate,
            // Address = new AddressDTO
            // {
            //     Id = x.Address.Id,
            //     Street = x.Address.Street,
            //     City = x.Address.City,
            //     State = x.Address.State,
            //     Country = x.Address.Country,
            //     ZipCode = x.Address.ZipCode
            // },
            BuyerId = x.BuyerId,
            OrderStatusId = x.OrderStatusId,
            Description = x.Description,
            IsDraft = x.IsDraft,
            OrderItems = x.OrderItems.Select(x => new OrderItemDTO
            {
                OrderItemId = x.OrderItemId,
                ProductName = x.ProductName,
                PictureUrl = x.PictureUrl,
                UnitPrice = x.UnitPrice,
                Discount = x.Discount,
                Units = x.Units,
                ProductId = x.ProductId
            }).ToList(),
            PaymentMethodId = x.PaymentMethodId
        });
        var result = !string.IsNullOrEmpty(id) ? all.Where(x=>x.OrderId.Equals(id)) : all;
        return Ok(result);
    }
}