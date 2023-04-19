using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using E_Commerce.Ordering.Models;

namespace E_Commerce.Ordering.Entities;

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
    public int PaymentStatus { get; set; }
    [NotMapped]
    public Payment Payment { get; set; }
}