using System.ComponentModel.DataAnnotations;

namespace E_Commerce.API.Entities;

public class BasketItem
{
    [Key] public Guid Id { get; set; }
    public int CatalogItemId { get; set; }
    public string CatalogItemName { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal OldUnitPrice { get; set; }
    public Guid BasketId { get; set; }
    public Basket Basket { get; set; }
    
}