
using EFRefresher.Context;
using Azure.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
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


app.MapGet("/customers", async (efrContext db) =>
{
    return await db.Customers.ToListAsync();
});



app.Run();

