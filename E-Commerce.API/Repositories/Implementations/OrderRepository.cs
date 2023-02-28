using System.Linq.Expressions;
using E_Commerce.API.Concrete;
using E_Commerce.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.API.Repositories;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(EFDBContext context) : base(context)
    {
    }
    
    public async Task<IEnumerable<Order>> GetAsync()
    {
       var data = await _context.Orders
           .Include(x=>x.OrderItems)
           .ToListAsync();
       return data;
    }

    public async Task<IEnumerable<Order>> GetAsync(Expression<Func<Order, bool>> predicate)
    {
        var data = await _context.Orders.Where(predicate)
            .Include(x=>x.OrderItems)
            .ToListAsync();
        return data;
    }

   
}