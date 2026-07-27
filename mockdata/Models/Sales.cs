namespace mockdata.Models;

public class Sales
{
    public int Cid { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public string UsCity { get; set; } = string.Empty;
    public string UsState { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public decimal TotalSales { get; set; }
    public DateTime CreatedDt { get; set; }
    public int Active { get; set; }
}
