using Microsoft.Extensions.Logging;

namespace FunctionApp.Services;

public interface ITrafficIngestionService
{
    Task RunAsync();
}

public sealed class TrafficIngestionService(ILogger<TrafficIngestionService> logger) : ITrafficIngestionService
{
    public Task RunAsync()
    {
        logger.LogInformation("Traffic ingestion called.");

        return Task.CompletedTask;
    }
}