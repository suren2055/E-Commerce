namespace E_Commerce.API.Models.DTO;

public class CustomerBasketDTO
{
    public string BuyerId { get; set; }

    public List<BasketItemDTO> Items { get; set; } = new();

    public CustomerBasketDTO()
    {

    }

    public CustomerBasketDTO(string customerId)
    {
        BuyerId = customerId;
    }
}