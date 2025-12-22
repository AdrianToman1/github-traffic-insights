using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FunctionApp.Functions;

public static class HealthCheck
{
    [Function(nameof(Alive))]
    public static async Task<HttpResponseData> Alive(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "alive")]
        HttpRequestData request,
        FunctionContext context)
    {
        var response = request.CreateResponse(HttpStatusCode.OK);
        await response.WriteStringAsync("alive");
        return response;
    }

    [Function(nameof(Health))]
    public static async Task<HttpResponseData> Health(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "health")]
        HttpRequestData request,
        FunctionContext context)
    {
        var healthService = context.InstanceServices.GetRequiredService<HealthCheckService>();
        var report = await healthService.CheckHealthAsync();

        var response = request.CreateResponse(
            report.Status == HealthStatus.Healthy
                ? HttpStatusCode.OK
                : HttpStatusCode.ServiceUnavailable);

        await response.WriteStringAsync(report.Status.ToString().ToLower());
        return response;
    }
}