using System.Text.Json;
using Microsoft.Extensions.Configuration;

Console.WriteLine("--- Start of Program ---");
 
// Load configuration from appsettings.json, user secrets, and environment variables
string subName = string.Empty;
string tenantId = string.Empty;
string contentSafetyEndpoint = string.Empty;
string projectEndpoint = string.Empty;
string modelDeploymentName = string.Empty;
string agentName = string.Empty;

LoadConfiguration();
GetEnvVars();

Console.WriteLine($"Project Endpoint from Config: {subName}");

Console.WriteLine("--- End of Program ---");
//////// MAIN Program END ////////

void GetEnvVars()
{
    projectEndpoint = Environment.GetEnvironmentVariable("PROJECT_ENDPOINT") ?? "<not set>";
    modelDeploymentName = Environment.GetEnvironmentVariable("MODEL_DEPLOYMENT_NAME") ?? "<not set>";
    contentSafetyEndpoint = Environment.GetEnvironmentVariable("CSAFE_ENDPOINT") ?? "<not set>";
    tenantId = Environment.GetEnvironmentVariable("TENANT_ID") ?? "<not set>";
    subName = Environment.GetEnvironmentVariable("SUB_NAME") ?? "<not set>";
    

    Console.WriteLine($"Project Endpoint: {projectEndpoint}");
    Console.WriteLine($"Model Deployment Name: {modelDeploymentName}");
    Console.WriteLine($"Content Safety Endpoint: {contentSafetyEndpoint}");
    Console.WriteLine($"Tenant ID: {tenantId}");
    Console.WriteLine($"Subscription Name: {subName}");
}

 void LoadConfiguration()
{
    var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddUserSecrets(typeof(Program).Assembly, optional: true)
        .AddEnvironmentVariables()
        .Build();



    agentName = config["AGENT_NAME"]
        ?? throw new InvalidOperationException("Missing AGENT_NAME");
    Console.WriteLine($"Agent Name from Config: {agentName}");


}
