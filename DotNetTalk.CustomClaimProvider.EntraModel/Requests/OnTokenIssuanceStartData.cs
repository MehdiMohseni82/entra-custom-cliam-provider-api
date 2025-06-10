using System.Text.Json.Serialization;

namespace DotNetTalk.CustomClaimProvider.EntraModel.Requests;

public class OnTokenIssuanceStartData
{
    [JsonPropertyName("@odata.type")]
    public string ODataType { get; set; }

    [JsonPropertyName("tenantId")]
    public string TenantId { get; set; }

    [JsonPropertyName("authenticationEventListenerId")]
    public string AuthenticationEventListenerId { get; set; }

    [JsonPropertyName("customAuthenticationExtensionId")]
    public string CustomAuthenticationExtensionId { get; set; }

    [JsonPropertyName("authenticationContext")]
    public AuthenticationContext AuthenticationContext { get; set; }
}