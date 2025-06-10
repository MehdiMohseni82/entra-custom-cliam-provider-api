using System.Text.Json.Serialization;

namespace DotNetTalk.CustomClaimProvider.EntraModel.Requests;

public class ServicePrincipal
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("appId")]
    public string AppId { get; set; }

    [JsonPropertyName("appDisplayName")]
    public string AppDisplayName { get; set; }

    [JsonPropertyName("displayName")]
    public string DisplayName { get; set; }
}