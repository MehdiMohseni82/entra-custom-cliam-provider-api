using System.Text.Json;
using Azure.Core.Serialization;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();


var config = new ConfigurationBuilder()
    .AddEnvironmentVariables()
    .Build();

builder.Services.AddSingleton(sp =>
{
    var options = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true,
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
    };

    return new JsonObjectSerializer(options);
});

builder.Logging.AddSerilog();

builder.Services.AddSingleton<HttpClient>();

builder.Services.AddSingleton<IOpenApiConfigurationOptions>(_ =>
{
    var options = new OpenApiConfigurationOptions()
    {
        Info = new OpenApiInfo()
        {
            Version = "1.0.0",
            Title = "DotNet Talk Identity Access Management API",
            Description = "The DotNet Talk Identity Access Management."
        },
        Servers = DefaultOpenApiConfigurationOptions.GetHostNames(),
        OpenApiVersion = OpenApiVersionType.V3,
        IncludeRequestingHostName = true,
        ForceHttps = false,
        ForceHttp = false,
    };

    return options;
});


var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
var logger = loggerFactory.CreateLogger("ServiceRegistration");

builder.Services.AddApplicationInsightsTelemetryWorkerService();
builder.Services.ConfigureFunctionsApplicationInsights();

var app = builder.Build();

app.Run();

