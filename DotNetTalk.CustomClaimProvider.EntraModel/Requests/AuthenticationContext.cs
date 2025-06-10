using System.Text.Json.Serialization;

namespace DotNetTalk.CustomClaimProvider.EntraModel.Requests;

public class AuthenticationContext
{
    [JsonPropertyName("correlationId")]
    public string CorrelationId { get; set; }

    [JsonPropertyName("client")]
    public Client Client { get; set; }

    [JsonPropertyName("protocol")]
    public string Protocol { get; set; }

    [JsonPropertyName("clientServicePrincipal")]
    public ServicePrincipal ClientServicePrincipal { get; set; }

    [JsonPropertyName("resourceServicePrincipal")]
    public ServicePrincipal ResourceServicePrincipal { get; set; }

    [JsonPropertyName("user")]
    public User User { get; set; }
}