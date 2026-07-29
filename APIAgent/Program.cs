using Azure.Identity;
using Azure.AI.Projects;
// using Azure.AI.Extensions.OpenAI;
// using OpenAI.Responses;
using Azure.AI.Projects.Agents;

Console.WriteLine("--- Start of Program ---");
 
// Load configuration from appsettings.json, user secrets, and environment variables
string subName;
string tenantId;
string contentSafetyEndpoint;
string projectEndpoint;
string modelDeploymentName;
string agentName = "SalesAPIAgent";

//LoadConfiguration();
GetEnvVars();

// Create Foundry Agent

// Create project client to call Foundry API
AIProjectClient projectClient = new(
    endpoint: new Uri(projectEndpoint),
    tokenProvider: new DefaultAzureCredential());

// Create an agent with a model and instructions
ProjectsAgentDefinition agentDefinition = new DeclarativeAgentDefinition(modelDeploymentName) // supports all Foundry direct models
{
    Instructions = "You are a helpful assistant that answers sales questions from a backend API.  When asked about a specific company, you will call the get_company_info function tool with the company id.",
};

ProjectsAgentVersion agent = projectClient.AgentAdministrationClient.CreateAgentVersion(
    agentName,
    options: new(agentDefinition));
Console.WriteLine($"Agent created (id: {agent.Id}, name: {agent.Name}, version: {agent.Version})");



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
