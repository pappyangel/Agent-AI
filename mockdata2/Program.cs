using mockdata2.Models;
using mockdata2.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddOpenApi();
builder.Services.AddSingleton<MockDataService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Minimal API endpoints
app.MapGet("/api/sales/getallsales", (MockDataService service,ILogger<Program> logger) =>
{
    logger.LogInformation("GetAllSales endpoint called");
    return Results.Ok(service.GetAllSales());
})
.WithName("GetAllSales")
.WithSummary("Returns all sales")
.WithDescription("Returns the complete list of sales records from the mock data service.")
.Produces<IReadOnlyList<Sales>>(StatusCodes.Status200OK);

app.MapGet("/api/sales/getsalesbycid/{cid:int}", (int cid, MockDataService service,ILogger<Program> logger) =>
{
    logger.LogInformation("GetSalesByCid endpoint called with CID: {Cid}", cid);
    var customersales = service.GetSalesByCid(cid);
    return customersales is null ? Results.NotFound() : Results.Ok(customersales);
})
.WithName("GetSalesByCid")
.WithSummary("Returns sales for a customer")
.WithDescription("Returns the sales record for the specified customer identifier if one exists.")
.Produces<Sales>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound);

app.MapPost("/api/sales/createmockdata", (MockDataService service, ILogger<Program> logger) =>
{
    logger.LogInformation("CreateMockData endpoint called");
    return Results.Ok(service.CreateMockData());
})
.WithName("CreateMockData")
.WithSummary("Creates mock sales data")
.WithDescription("Generates a fresh set of mock sales records and returns them.")
.Produces<IReadOnlyList<Sales>>(StatusCodes.Status200OK);

app.MapGet("/", (ILogger<Program> logger) =>
{
    logger.LogInformation("GetServerInfo endpoint called");
    var response = new
    {
        machineName = Environment.MachineName,
        timestamp = DateTime.UtcNow
    };
    logger.LogInformation("Returning server info: {MachineName}, {Timestamp}", response.machineName, response.timestamp);
    return Results.Ok(response);
})
.WithName("GetServerInfo")
.WithSummary("Returns information about the API host")
.WithDescription("Returns server information including machine name and current UTC timestamp.");

app.Run();
