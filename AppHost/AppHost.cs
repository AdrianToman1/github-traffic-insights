var builder = DistributedApplication.CreateBuilder(args);

builder.AddAzureFunctionsProject<Projects.FunctionApp>("functionapp");

builder.Build().Run();
