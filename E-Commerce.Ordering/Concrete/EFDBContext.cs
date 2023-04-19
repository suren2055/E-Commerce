using E_Commerce.Ordering.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Ordering.Concrete;

public class EFDBContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItem { get; set; }
    public DbSet<Address> Addresses { get; set; }
    
    
    public EFDBContext(DbContextOptions<EFDBContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<Order>()
        //     .HasMany(o => o.OrderItems)
        //     .WithOne(i => Order)
        //     .HasForeignKey(i => i.OrderId);
        //
        // modelBuilder.Entity<Order>()
        //     .HasMany(o => o.OrderItems)
        //     .WithOne(i => Order)
        //     .IsRequired()
        //     .OnDelete(DeleteBehavior.Cascade);
        
       

        base.OnModelCreating(modelBuilder);
    }
}