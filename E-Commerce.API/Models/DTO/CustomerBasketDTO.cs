namespace E_Commerce.API.Models.DTO;

public class CustomerBasketDTO
{
    public Guid BuyerId { get; set; }

    public List<BasketItemDTO> Items { get; set; } = new();

    public CustomerBasketDTO()
    {

    }

    public CustomerBasketDTO(Guid customerId)
    {
        BuyerId = customerId;
    }
}