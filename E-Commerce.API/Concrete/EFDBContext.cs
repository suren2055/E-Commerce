using E_Commerce.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.API.Concrete;

public class EFDBContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<CatalogItem> CatalogItems { get; set; }
    
    public EFDBContext(DbContextOptions<EFDBContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}