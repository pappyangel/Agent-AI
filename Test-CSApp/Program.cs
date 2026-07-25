using System.Text.Json;
using Microsoft.Extensions.Configuration;

Console.WriteLine("Start of Program");

var (projectEndpoint, modelDeploymentName, contentsafetyEndpoint, agentName) = LoadConfiguration();

GetEnvVars();

Console.WriteLine("End of Program");
//////// MAIN Program END ////////

void GetEnvVars()
{
    string projectEndpoint = Environment.GetEnvironmentVariable("PROJECT_ENDPOINT") ?? "<not set>";
    string modelDeploymentName = Environment.GetEnvironmentVariable("MODEL_DEPLOYMENT_NAME") ?? "<not set>";
    string contentSafetyEndpoint = Environment.GetEnvironmentVariable("CSAFE_ENDPOINT") ?? "<not set>";
    string tenantId = Environment.GetEnvironmentVariable("TENANT_ID") ?? "<not set>";
    string subName = Environment.GetEnvironmentVariable("SUB_NAME") ?? "<not set>";

    Console.WriteLine($"Project Endpoint: {projectEndpoint}");
    Console.WriteLine($"Model Deployment Name: {modelDeploymentName}");
    Console.WriteLine($"Content Safety Endpoint: {contentSafetyEndpoint}");
    Console.WriteLine($"Tenant ID: {tenantId}");
    Console.WriteLine($"Subscription Name: {subName}");
}

// Load configuration from appsettings, user secrets, and environment variables
(string, string, string, string) LoadConfiguration()
{
    var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddUserSecrets(typeof(Program).Assembly, optional: true)
        .AddEnvironmentVariables()
        .Build();

    string projectEndpointFromConfig = config["PROJECT_ENDPOINT"]
        ?? throw new InvalidOperationException("Missing PROJECT_ENDPOINT");

    string modelDeploymentNameFromConfig = config["MODEL_DEPLOYMENT_NAME"]
        ?? throw new InvalidOperationException("Missing MODEL_DEPLOYMENT_NAME");

    string contentsafetyEndpointFromConfig = config["CSAFE_ENDPOINT"]
        ?? throw new InvalidOperationException("Missing CSAFE_ENDPOINT");

    string agentName = config["AGENT_NAME"]
        ?? throw new InvalidOperationException("Missing AGENT_NAME");

    return (projectEndpointFromConfig, modelDeploymentNameFromConfig, contentsafetyEndpointFromConfig, agentName);
}
