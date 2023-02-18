using System.Linq.Expressions;
using E_Commerce.API.Concrete;
using E_Commerce.API.Entities;

namespace E_Commerce.API.Repositories;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(EFDBContext context) : base(context)
    {
    }
}