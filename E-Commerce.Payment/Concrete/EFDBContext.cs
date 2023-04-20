using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Payment.Concrete;

public class EFDBContext : DbContext
{
    public DbSet<Entities.Payment> Payments { get; set; }

    
    
    public EFDBContext(DbContextOptions<EFDBContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}