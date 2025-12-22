using System.Net;
using FunctionApp.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace FunctionApp.Functions;

public class ManualTrigger(ITrafficIngestionService service, ILogger<ManualTrigger> logger)
{
    [Function(nameof(ManualTrigger))]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
    {
        logger.LogInformation("Manual trigger invoked");
        try
        {
            await service.RunAsync();
            logger.LogInformation("Manual trigger completed successfully");
            return req.CreateResponse(HttpStatusCode.NoContent);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Manual trigger failed");
            throw;
        }
    }
}