using Microsoft.Extensions.Configuration;

using Azure.Identity;
using Azure.AI.Projects;
using Azure.AI.Extensions.OpenAI;
using OpenAI.Responses;

#pragma warning disable OPENAI001

Console.WriteLine("Start of Program");


var (projectEndpoint, modelDeploymentName, contentsafetyEndpoint) = LoadConfiguration();

// Console.WriteLine($"Project Endpoint: {projectEndpoint}");
// Console.WriteLine($"Model Deployment Name: {modelDeploymentName}");
// Console.WriteLine($"Content Safety Endpoint: {contentsafetyEndpoint}");

// var subNameLive = Environment.GetEnvironmentVariable("SUB_NAME_LIVE");
// Console.WriteLine(subNameLive);


// Create project client to call Foundry API
AIProjectClient projectClient = new(
    endpoint: new Uri(projectEndpoint),
    tokenProvider: new DefaultAzureCredential());

// Run a responses API call
ProjectResponsesClient responseClient = projectClient.ProjectOpenAIClient.GetProjectResponsesClientForModel(modelDeploymentName); 
ResponseResult response = await responseClient.CreateResponseAsync(
    "What is the size of France in square miles?");
Console.WriteLine(response.GetOutputText());

Console.WriteLine("End of Program");
//////// MAIN Program END ////////

// Load configuration from appsettings, user secrets, and environment variables
(string, string, string) LoadConfiguration()
{
    var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddUserSecrets(typeof(Program).Assembly, optional: true)
        .AddEnvironmentVariables()
        .Build();

    string projectEndpoint = config["AI_RESOURCE_ENDPOINT"]
        ?? throw new InvalidOperationException("Missing AI_RESOURCE_ENDPOINT");

    string modelDeploymentName = config["MODEL_DEPLOYMENT_NAME"]
        ?? throw new InvalidOperationException("Missing MODEL_DEPLOYMENT_NAME");

    string contentsafetyEndpoint = config["CSAFE_ENDPOINT"]
        ?? throw new InvalidOperationException("Missing CSAFE_ENDPOINT");

    return (projectEndpoint, modelDeploymentName, contentsafetyEndpoint);
}

