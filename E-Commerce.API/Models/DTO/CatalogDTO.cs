namespace E_Commerce.API.Models.DTO;

public class CatalogDTO
{
    public int PageIndex { get; init; }
    public int PageSize { get; init; }
    public int Count { get; init; }
    public List<CatalogItemDTO> Data { get; init; }
}