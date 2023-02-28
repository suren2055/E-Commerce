namespace E_Commerce.API.Models.DTO;

public class OrderDTO
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public AddressDTO Address { get; set; }
    public Guid? BuyerId { get; set; }
    public int OrderStatusId { get; set; }
    public string Description { get; set; }
    public bool IsDraft { get; set; }
    public ICollection<OrderItemDTO> OrderItems { get; set; }
    public int? PaymentMethodId { get; set; }
}

public class AddressDTO
{
  
    public int Id { get; set; }
    public string Street { get;  set; }
    public string City { get;  set; }
    public string State { get;  set; }
    public string Country { get;  set; }
    public string ZipCode { get;  set; }
}

public class OrderItemDTO
{
    public int OrderItemId { get; set; }
    public string ProductName { get; set; }
    public string PictureUrl { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public int Units { get; set; }
    public int ProductId { get; set; }
   
}

