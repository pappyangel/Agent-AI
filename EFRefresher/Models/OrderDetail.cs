using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace EFRefresher.Models;

public class OrderDetail
{
    [Key]
    public int OrderDetailId { get; set; }
    public int OrderId { get; set; }

    public int ProductId { get; set; }
    public int Quantity { get; set; }
    
    [Precision(18, 2)]
    public decimal UnitPrice { get; set; }
    
    [Precision(18, 2)]
    public decimal LineTotal { get; set; }

    // Navigation property
    public Order? Order { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

}
