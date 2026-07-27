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
.WithOpenApi()
.Produces<IReadOnlyList<object>>(StatusCodes.Status200OK);

app.MapGet("/api/sales/getsalesbycid/{cid:int}", (int cid, MockDataService service) =>
{
    var sale = service.GetSalesByCid(cid);
    return sale is null ? Results.NotFound() : Results.Ok(sale);
})
.WithName("GetSalesByCid")
.WithOpenApi()
.Produces<object>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound);

app.MapPost("/api/sales/createmockdata", (MockDataService service) =>
{
    return Results.Ok(service.CreateMockData());
})
.WithName("CreateMockData")
.WithOpenApi()
.Produces<IReadOnlyList<object>>(StatusCodes.Status200OK);

app.Run();
