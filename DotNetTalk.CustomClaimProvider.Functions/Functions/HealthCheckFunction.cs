using System.Net;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;

namespace DotNetTalk.CustomClaimProvider.Functions.Functions;

public class HealthCheckFunction(ILogger<HealthCheckFunction> logger)
{
    [Function(nameof(HealthCheckFunction))]
    [OpenApiOperation(operationId: "HealthCheck", tags: [ "Health" ], Summary = "Health Check", Description = "Checks if the service is running and healthy.")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/json", bodyType: typeof(string), Description = "Returns a response containing the status and the current version of the service.")]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.InternalServerError, Description = "An unexpected error occurred.")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "health")] HttpRequest req)
    {
        logger.LogInformation("Health Check Received!");

        return new OkObjectResult("Healthy");
    }
}
