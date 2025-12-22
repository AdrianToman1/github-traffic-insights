using FunctionApp.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionApp.Functions;

public class TimerTrigger(ITrafficIngestionService service, TimeProvider timeProvider, ILogger<TimerTrigger> logger)
{
    [Function(nameof(TimerTrigger))]
    public async Task Run([TimerTrigger("%TrafficIngestion:Schedule%")] TimerInfo timer)
    {
        logger.LogInformation("Timer trigger started at {Timestamp}", timeProvider.GetUtcNow());
        try
        {
            await service.RunAsync();
            logger.LogInformation("Timer trigger completed successfully");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Timer trigger failed");
            throw;
        }
    }
}