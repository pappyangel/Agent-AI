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
app.MapGet("/api/sales/getallsales", (MockDataService service) =>
{
    return Results.Ok(service.GetAllSales());
})
.WithName("GetAllSales")
.Produces<IReadOnlyList<object>>(StatusCodes.Status200OK);

app.MapGet("/api/sales/getsalesbycid/{cid:int}", (int cid, MockDataService service) =>
{
    var sale = service.GetSalesByCid(cid);
    return sale is null ? Results.NotFound() : Results.Ok(sale);
})
.WithName("GetSalesByCid")
.Produces<object>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound);

app.MapPost("/api/sales/createmockdata", (MockDataService service) =>
{
    return Results.Ok(service.CreateMockData());
})
.WithName("CreateMockData")

.Produces<IReadOnlyList<object>>(StatusCodes.Status200OK);

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
