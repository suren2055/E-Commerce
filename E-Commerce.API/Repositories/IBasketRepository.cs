using E_Commerce.API.Models.DTO;

namespace E_Commerce.API.Repositories;

public interface IBasketRepository
{
    Task<CustomerBasketDTO> GetBasketAsync(string customerId);
    IEnumerable<string> GetUsers();
    Task<CustomerBasketDTO> UpdateBasketAsync(CustomerBasketDTO basketDto);
    Task<bool> DeleteBasketAsync(string id);
}