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

}
