using E_Commerce.Ordering.Concrete;
using E_Commerce.Ordering.Entities;

namespace E_Commerce.Ordering.Repositories;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(EFDBContext context) : base(context)
    {
        
    }
}