using AppHost;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddAzureFunctionsProject<FunctionApp>(Names.FunctionApp)
    .WithEnvironment("TrafficIngestion__Schedule", "0 */5 * * * *")
    .WithHttpCommand("/ManualTrigger", "Run Ingestion Now");

builder.Build().Run();