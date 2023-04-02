using System.Text.Json;
using E_Commerce.API.Concrete;
using E_Commerce.API.Entities;
using E_Commerce.API.Models.DTO;
using StackExchange.Redis;

namespace E_Commerce.API.Repositories;

public class BasketRepository : BaseRepository<Basket>, IBasketRepository
{
    private readonly ILogger<BasketRepository> _logger;
    private readonly ConnectionMultiplexer _redis;
    private readonly IDatabase _database;


    public BasketRepository(EFDBContext context, ILoggerFactory loggerFactory, ConnectionMultiplexer redis) :
        base(context)
    {
        _logger = loggerFactory.CreateLogger<BasketRepository>();
        _redis = redis;
        _database = redis.GetDatabase();
    }

    public async Task<bool> DeleteBasketAsync(Guid id)
    {
        return await _database.KeyDeleteAsync(id.ToString());
    }

    public IEnumerable<string> GetUsers()
    {
        var server = GetServer();
        var data = server.Keys();
        return data?.Select(k => k.ToString());
    }

    public async Task<CustomerBasketDTO> GetBasketAsync(Guid customerId)
    {
        var data = await _database.StringGetAsync(customerId.ToString());
        //when the data in the cache has been expired we retrieve it from the db and add it into the cache

        if (!data.IsNullOrEmpty)
            return JsonSerializer.Deserialize<CustomerBasketDTO>(data, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        var basket = await FindAsync(customerId);
        if (basket == null) return null;
        var dto = new CustomerBasketDTO
        {
            BuyerId = basket.BasketId,
            Items = basket.BasketItems.Select(x => new BasketItemDTO
            {
                Id = x.Id.ToString(),
                UnitPrice = x.UnitPrice,
                ProductId = x.CatalogItemId,
                ProductName = x.CatalogItemName,
                OldUnitPrice = x.OldUnitPrice,
                
            }).ToList()
        };
        await _database.StringSetAsync(customerId.ToString(), JsonSerializer.Serialize(dto));

        return JsonSerializer.Deserialize<CustomerBasketDTO>(data, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }

    public async Task<CustomerBasketDTO> UpdateBasketAsync(CustomerBasketDTO basketDto)
    {
        var createdInCache = await _database.StringSetAsync(basketDto.BuyerId.ToString(), JsonSerializer.Serialize(basketDto));
        var entity = new Basket
        {
            BasketId = basketDto.BuyerId,
            BasketItems = basketDto.Items.Select(x => new BasketItem
            {
                Id = Guid.Parse(x.Id),
                UnitPrice = x.UnitPrice,
                CatalogItemId = x.ProductId,
                CatalogItemName = x.ProductName,
                OldUnitPrice = x.OldUnitPrice,
                BasketId = basketDto.BuyerId,
                Basket = new Basket
                {
                    BasketId = basketDto.BuyerId
                }
                
                
            }).ToList()
        };
        try
        {
            object k = await FindAsync(basketDto.BuyerId);
         
            if (k != null)
                //update
                k = Update(entity);
            else
                //insert
                k = await CreateAsync(entity);
            if (!createdInCache && k != null)
            {
                _logger.LogInformation("Problem occur persisting the item.");
                return null;
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
      

       

        _logger.LogInformation("Basket item persisted succesfully.");
        return await GetBasketAsync(basketDto.BuyerId);
    }

    private IServer GetServer()
    {
        var endpoint = _redis.GetEndPoints();
        return _redis.GetServer(endpoint.First());
    }
}