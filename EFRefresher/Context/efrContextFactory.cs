using Azure.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EFRefresher.Context;

public class efrContextFactory : IDesignTimeDbContextFactory<efrContext>
{
    public efrContext CreateDbContext(string[] args)
    {
        // Load configuration the same way Program.cs does
        var env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Development";

        var configBuilder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile($"appsettings.{env}.json", optional: true)
            .AddUserSecrets(typeof(efrContextFactory).Assembly);


        var config = configBuilder.Build();

        var baseConn = config.GetConnectionString("SqlDb");
        var dbName = config["DatabaseName"];
        var connectionString = $"{baseConn};Database={dbName}";

        // Acquire AAD token
        var credential = new DefaultAzureCredential();
        var token = credential.GetToken(
            new Azure.Core.TokenRequestContext(new[] { "https://database.windows.net/.default" })
        );

        var sqlConnection = new SqlConnection(connectionString);
        sqlConnection.AccessToken = token.Token;

        var optionsBuilder = new DbContextOptionsBuilder<efrContext>();
        optionsBuilder.UseSqlServer(sqlConnection);

        return new efrContext(optionsBuilder.Options);
    }
}
