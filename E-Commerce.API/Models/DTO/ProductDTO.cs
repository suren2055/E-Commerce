namespace E_Commerce.API.Models.DTO;

public class ProductDTO
{
    public Guid ProductId { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public CategoryDTO Category { get; set; }
    
    
}