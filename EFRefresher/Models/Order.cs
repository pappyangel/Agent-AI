using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace EFRefresher.Models;

public class Order
{
    [Key]
    public int OrderId { get; set; }

    public int CustomerId { get; set; }

    public DateTime OrderDate { get; set; }

    [Precision(18, 2)]
    public decimal TotalAmount { get; set; }

    // Navigation properties
    public Customer? Customer { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}