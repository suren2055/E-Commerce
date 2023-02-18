using E_Commerce.API.Concrete;
using E_Commerce.API.Entities;

namespace E_Commerce.API.Repositories;

public class CatalogItemRepository : BaseRepository<CatalogItem>, ICatalogItemRepository
{
    public CatalogItemRepository(EFDBContext context) : base(context)
    {
    }
}