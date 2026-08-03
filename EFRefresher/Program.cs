﻿using Azure.Identity;
using EFRefresher.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


using Microsoft.Extensions.Configuration;

var env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production";

Console.WriteLine($"Current Environment: {env}");

var configBuilder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{env}.json", optional: true);

if (env == "Development")
{
    configBuilder.AddUserSecrets<Program>();
}
var config = configBuilder.Build();

Console.WriteLine($"Configuration Loaded: {config["dog"]}");


// Your SQL Azure connection string without credentials
// var baseConn = config.GetConnectionString("SqlDb2");
// var dbName = config["DatabaseName"];
// var connectionString = $"{baseConn};Database={dbName}";

var connectionString = config.GetConnectionString("SqlDb");

Console.WriteLine($"Connection String: {connectionString}");

// Environment.Exit(0);

// Get an access token using DefaultAzureCredential
var credential = new DefaultAzureCredential();
var token = credential.GetToken(
    new Azure.Core.TokenRequestContext(new[] { "https://database.windows.net/.default" })
);

// Build the SQL connection with the token
var sqlConnection = new SqlConnection(connectionString);
sqlConnection.AccessToken = token.Token;

// Configure EF Core to use the authenticated connection
var optionsBuilder = new DbContextOptionsBuilder<efrContext>();
optionsBuilder.UseSqlServer(sqlConnection);


// Various EF commands for testing the connection and querying data
using var context = new efrContext(optionsBuilder.Options);

var count = context.Orders.Count();
Console.WriteLine($"Orders in DB: {count}");

var customerWithOrders = await context.Customers
    .Include(c => c.Orders)
    .FirstOrDefaultAsync(c => c.CustomerId == 1011);

if (customerWithOrders != null)
{
    Console.WriteLine($"Customer: {customerWithOrders.CustomerName}");
    foreach (var order in customerWithOrders.Orders)
    {
        Console.WriteLine($"  Order {order.OrderId} on {order.OrderDate}, Total: {order.TotalAmount}");
    }
}
else
{
    Console.WriteLine("Customer not found.");
}




Console.WriteLine("Check. EF setup created using DefaultAzureCredential.");
