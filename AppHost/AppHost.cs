using Projects;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddAzureFunctionsProject<FunctionApp>(nameof(FunctionApp))
    .WithEnvironment("TrafficIngestion__Schedule", "0 */5 * * * *")
    .WithHttpCommand("/ManualTrigger", "Run Ingestion Now");

builder.Build().Run();