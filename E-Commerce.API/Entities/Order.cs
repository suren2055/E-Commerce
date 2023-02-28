using System.ComponentModel.DataAnnotations;

namespace E_Commerce.API.Entities;

public class Order
{
    [Key]
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public Address Address { get; set; }
    public Guid? BuyerId { get; set; }
    public int OrderStatusId { get; set; }
    public string Description { get; set; }
    public bool IsDraft { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
    public int? PaymentMethodId { get; set; }
}