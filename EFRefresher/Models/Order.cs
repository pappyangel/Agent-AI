namespace EFRefresher.Models;

public class Order
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }

    // Navigation property
    public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
