using System.ComponentModel.DataAnnotations;

namespace EFRefresher.Models;

public class Customer
{
    [Key]
    public int CustomerID { get; set; }

    [StringLength(30)]
    public string? CustomerName { get; set; }

    // Navigation property
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
