using System.Text.Json;
using mockdata.Models;

namespace mockdata.Services;

public class MockDataService
{
    private const int RecordCount = 50;
    private static readonly DateTime MinDate = new(2020, 1, 1);
    private static readonly DateTime MaxDate = new(2026, 12, 31);

    private static readonly string[] ClientNames =
    {
        "Acme Corp", "Globex Industries", "Initech", "Umbrella Group", "Stark Enterprises",
        "Wayne Holdings", "Wonka Ventures", "Hooli Inc", "Soylent Systems", "Massive Dynamic",
        "Cyberdyne Systems", "Oscorp", "Pied Piper", "Aperture Science", "Gringotts Group",
        "Dunder Mifflin", "Vandelay Industries", "Sterling Cooper", "Prestige Worldwide", "Bluth Company"
    };

    private static readonly (string City, string State)[] Locations =
    {
        ("New York", "New York"), ("Los Angeles", "California"), ("Chicago", "Illinois"),
        ("Houston", "Texas"), ("Phoenix", "Arizona"), ("Philadelphia", "Pennsylvania"),
        ("San Antonio", "Texas"), ("San Diego", "California"), ("Dallas", "Texas"),
        ("Austin", "Texas"), ("Jacksonville", "Florida"), ("Columbus", "Ohio"),
        ("Charlotte", "North Carolina"), ("Seattle", "Washington"), ("Denver", "Colorado"),
        ("Boston", "Massachusetts"), ("Nashville", "Tennessee"), ("Portland", "Oregon"),
        ("Atlanta", "Georgia"), ("Miami", "Florida")
    };

    private static readonly string[] Statuses = { "Active", "Pending", "Closed", "OnHold", "Cancelled" };

    private readonly string _dataFilePath;
    private readonly List<Sales> _sales;

    public MockDataService(IWebHostEnvironment env)
    {
        var dataDir = Path.Combine(env.ContentRootPath, "Data");
        Directory.CreateDirectory(dataDir);
        _dataFilePath = Path.Combine(dataDir, "salesdata.json");

        _sales = File.Exists(_dataFilePath)
            ? LoadFromFile()
            : CreateMockData();
    }

    public IReadOnlyList<Sales> GetAllSales() => _sales;

    public Sales? GetSalesByCid(int cid) => _sales.FirstOrDefault(s => s.Cid == cid);

    public List<Sales> CreateMockData()
    {
        var random = new Random();
        var generated = new List<Sales>(RecordCount);

        for (var i = 1; i <= RecordCount; i++)
        {
            var location = Locations[random.Next(Locations.Length)];

            generated.Add(new Sales
            {
                Cid = i,
                ClientName = ClientNames[random.Next(ClientNames.Length)],
                UsCity = location.City,
                UsState = location.State,
                Status = Statuses[random.Next(Statuses.Length)],
                TotalSales = Math.Round((decimal)(random.NextDouble() * 50000), 2),
                CreatedDt = RandomDate(random),
                Active = random.Next(0, 2)
            });
        }

        SaveToFile(generated);
        return generated;
    }

    private static DateTime RandomDate(Random random)
    {
        var range = (MaxDate - MinDate).Days;
        return MinDate.AddDays(random.Next(range + 1)).AddSeconds(random.Next(0, 86400));
    }

    private void SaveToFile(List<Sales> sales)
    {
        var json = JsonSerializer.Serialize(sales, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_dataFilePath, json);
    }

    private List<Sales> LoadFromFile()
    {
        var json = File.ReadAllText(_dataFilePath);
        return JsonSerializer.Deserialize<List<Sales>>(json) ?? new List<Sales>();
    }
}
