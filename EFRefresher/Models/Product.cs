using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace EFRefresher.Models;

public class Product
{
    [Key]
    public int ProductId { get; set; }

    [StringLength(50)]
    public string? ProductName { get; set; }

    [StringLength(200)]
    public string? Description { get; set; }

    [Precision(18, 2)]
    public decimal UnitPrice { get; set; }

    public bool IsActive { get; set; } = true;

    // Navigation property (optional)
    public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}