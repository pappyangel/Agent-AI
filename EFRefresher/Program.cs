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

using var context = new efrContext(optionsBuilder.Options);

Console.WriteLine("Testing EF Core connection...");
var count = context.Orders.Count();
Console.WriteLine($"Orders in DB: {count}");

// context.Database.Migrate();

Console.WriteLine("Check. EF setup created using DefaultAzureCredential.");
