using System.Text.Json;
using E_Commerce.API.Models.DTO;
using StackExchange.Redis;

namespace E_Commerce.API.Repositories;

public class RedisBasketRepository : IBasketRepository
{
    private readonly ILogger<RedisBasketRepository> _logger;
    private readonly ConnectionMultiplexer _redis;
    private readonly IDatabase _database;

    public RedisBasketRepository(ILoggerFactory loggerFactory, ConnectionMultiplexer redis)
    {
        _logger = loggerFactory.CreateLogger<RedisBasketRepository>();
        _redis = redis;
        _database = redis.GetDatabase();
    }

    public async Task<bool> DeleteBasketAsync(string id)
    {
        return await _database.KeyDeleteAsync(id);
    }

    public IEnumerable<string> GetUsers()
    {
        var server = GetServer();
        var data = server.Keys();
        return data?.Select(k => k.ToString());
    }

    public async Task<CustomerBasketDTO> GetBasketAsync(string customerId)
    {
        var data = await _database.StringGetAsync(customerId);
        if (data.IsNullOrEmpty) return null;
      
        return JsonSerializer.Deserialize<CustomerBasketDTO>(data, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        
    }

    public async Task<CustomerBasketDTO> UpdateBasketAsync(CustomerBasketDTO basketDto)
    {
        
        var created = await _database.StringSetAsync(basketDto.BuyerId, JsonSerializer.Serialize(basketDto));

        if (!created)
        {
            _logger.LogInformation("Problem occur persisting the item.");
            return null;
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