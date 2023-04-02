using System.ComponentModel.DataAnnotations;

namespace E_Commerce.API.Entities;

public class Basket
{
    [Key]
    public Guid BasketId { get; set; }
    public ICollection<BasketItem> BasketItems { get; set; }
}