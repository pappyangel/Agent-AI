using Microsoft.EntityFrameworkCore;
using EFRefresher.Models;

namespace EFRefresher.Context;

public partial class efrContext : DbContext
{
    public efrContext(DbContextOptions<efrContext> options)
        : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>()
          .Property(c => c.CustomerId)
          .UseIdentityColumn(seed: 1000, increment: 1);

        modelBuilder.Entity<Product>()
            .Property(p => p.ProductId)
            .UseIdentityColumn(seed: 1000, increment: 1);

        modelBuilder.Entity<Order>()
            .Property(o => o.OrderId)
            .UseIdentityColumn(seed: 1000, increment: 1);

        modelBuilder.Entity<OrderDetail>()
            .Property(od => od.OrderDetailId)
            .UseIdentityColumn(seed: 1000, increment: 1);

        // You can add other configuration here later
        // e.g. relationships, precision, indexes, etc.
    }

}
