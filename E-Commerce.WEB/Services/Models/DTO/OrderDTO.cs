using System.ComponentModel.DataAnnotations;

namespace E_Commerce.WEB.Services.Models.DTO;

public record OrderDTO
{
    [Required]
    public string OrderNumber { get; init; }
}