
using EFRefresher.Context;
using Azure.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// openAPI spec: /openapi/v1.json
builder.Services.AddOpenApi();

builder.Services.AddDbContext<efrContext>(options =>
{
    var config = builder.Configuration;

    var baseConn = config.GetConnectionString("SqlDb");
    var dbName = config["DatabaseName"];
    var connectionString = $"{baseConn};Database={dbName}";

    var credential = new DefaultAzureCredential();
    var token = credential.GetToken(
        new Azure.Core.TokenRequestContext(new[] { "https://database.windows.net/.default" })
    );

    var sqlConnection = new SqlConnection(connectionString);
    sqlConnection.AccessToken = token.Token;

    options.UseSqlServer(sqlConnection);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

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


// =========================
// Customers
// =========================

app.MapGet("/customers", async (efrContext db, ILogger<Program> logger) =>
{
    logger.LogInformation("GET /customers called");
    var customers = await db.Customers.ToListAsync();
    logger.LogInformation("Retrieved {Count} customers", customers.Count);
    return Results.Ok(customers);
})
.WithName("GetCustomers")
.WithSummary("Returns all customers")
.WithDescription("Fetches all customer records from the database.")
.WithTags("Customers");

app.MapGet("/customers/{id:int}", async (int id, efrContext db, ILogger<Program> logger) =>
{
    logger.LogInformation("GET /customers/{Id} called", id);
    var customer = await db.Customers.FindAsync(id);
    if (customer is null)
    {
        logger.LogWarning("Customer {Id} not found", id);
        return Results.NotFound();
    }
    logger.LogInformation("Retrieved customer {Id}", id);
    return Results.Ok(customer);
})
.WithName("GetCustomerById")
.WithSummary("Returns a customer by ID")
.WithDescription("Fetches a single customer record using its unique identifier.")
.WithTags("Customers");


// =========================
// Products
// =========================

app.MapGet("/products", async (efrContext db, ILogger<Program> logger) =>
{
    logger.LogInformation("GET /products called");
    var products = await db.Products.ToListAsync();
    logger.LogInformation("Retrieved {Count} products", products.Count);
    return Results.Ok(products);
})
.WithName("GetProducts")
.WithSummary("Returns all products")
.WithDescription("Fetches all product records from the database.")
.WithTags("Products");

app.MapGet("/products/{id:int}", async (int id, efrContext db, ILogger<Program> logger) =>
{
    logger.LogInformation("GET /products/{Id} called", id);
    var product = await db.Products.FindAsync(id);
    if (product is null)
    {
        logger.LogWarning("Product {Id} not found", id);
        return Results.NotFound();
    }
    logger.LogInformation("Retrieved product {Id}", id);
    return Results.Ok(product);
})
.WithName("GetProductById")
.WithSummary("Returns a product by ID")
.WithDescription("Fetches a single product record using its unique identifier.")
.WithTags("Products");


// =========================
// Orders
// =========================

app.MapGet("/orders", async (efrContext db, ILogger<Program> logger) =>
{
    logger.LogInformation("GET /orders called");
    var orders = await db.Orders.ToListAsync();
    logger.LogInformation("Retrieved {Count} orders", orders.Count);
    return Results.Ok(orders);
})
.WithName("GetOrders")
.WithSummary("Returns all orders")
.WithDescription("Fetches all order records from the database.")
.WithTags("Orders");

app.MapGet("/orders/{id:int}", async (int id, efrContext db, ILogger<Program> logger) =>
{
    logger.LogInformation("GET /orders/{Id} called", id);
    var order = await db.Orders.FindAsync(id);
    if (order is null)
    {
        logger.LogWarning("Order {Id} not found", id);
        return Results.NotFound();
    }
    logger.LogInformation("Retrieved order {Id}", id);
    return Results.Ok(order);
})
.WithName("GetOrderById")
.WithSummary("Returns an order by ID")
.WithDescription("Fetches a single order record using its unique identifier.")
.WithTags("Orders");


// =========================
// OrderDetails
// =========================

app.MapGet("/orderdetails", async (efrContext db, ILogger<Program> logger) =>
{
    logger.LogInformation("GET /orderdetails called");
    var details = await db.OrderDetails.ToListAsync();
    logger.LogInformation("Retrieved {Count} order details", details.Count);
    return Results.Ok(details);
})
.WithName("GetOrderDetails")
.WithSummary("Returns all order details")
.WithDescription("Fetches all order detail records from the database.")
.WithTags("OrderDetails");

app.MapGet("/orderdetails/{id:int}", async (int id, efrContext db, ILogger<Program> logger) =>
{
    logger.LogInformation("GET /orderdetails/{Id} called", id);
    var detail = await db.OrderDetails.FindAsync(id);
    if (detail is null)
    {
        logger.LogWarning("OrderDetail {Id} not found", id);
        return Results.NotFound();
    }
    logger.LogInformation("Retrieved order detail {Id}", id);
    return Results.Ok(detail);
})
.WithName("GetOrderDetailById")
.WithSummary("Returns an order detail by ID")
.WithDescription("Fetches a single order detail record using its unique identifier.")
.WithTags("OrderDetails");



app.Run();

