using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Ordering.Entities;

public class OrderItem
{
    [Key]
    public int OrderItemId { get; set; }
    public string ProductName { get; set; }
    public string PictureUrl { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public int Units { get; set; }
    public int ProductId { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; }
}