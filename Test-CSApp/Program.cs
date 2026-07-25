using System.Text.Json;
using Microsoft.Extensions.Configuration;

Console.WriteLine("Start of Program");


// Load configuration and get values
var (projectEndpoint, modelDeploymentName, contentsafetyEndpoint, agentName) = LoadConfiguration();

Console.WriteLine($"Project Endpoint: {projectEndpoint}");
Console.WriteLine($"Model Deployment Name: {modelDeploymentName}");
Console.WriteLine($"Content Safety Endpoint: {contentsafetyEndpoint}");
Console.WriteLine($"Agent Name: {agentName}");

var subNameLive = Environment.GetEnvironmentVariable("SUB_NAME_LIVE");
Console.WriteLine(subNameLive);

Console.WriteLine("End of Program");
//////// MAIN Program END ////////

// Load configuration from appsettings, user secrets, and environment variables
(string, string, string, string) LoadConfiguration()
{
    var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddUserSecrets(typeof(Program).Assembly, optional: true)
        .AddEnvironmentVariables()
        .Build();

    string projectEndpoint = config["PROJECT_ENDPOINT"]
        ?? throw new InvalidOperationException("Missing PROJECT_ENDPOINT");

    string modelDeploymentName = config["MODEL_DEPLOYMENT_NAME"]
        ?? throw new InvalidOperationException("Missing MODEL_DEPLOYMENT_NAME");

    string contentsafetyEndpoint = config["CSAFE_ENDPOINT"]
        ?? throw new InvalidOperationException("Missing CSAFE_ENDPOINT");

    string agentName = config["AGENT_NAME"]
        ?? throw new InvalidOperationException("Missing AGENT_NAME");

    return (projectEndpoint, modelDeploymentName, contentsafetyEndpoint, agentName);
}
