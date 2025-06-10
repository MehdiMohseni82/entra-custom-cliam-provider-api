using System.Text.Json.Serialization;

namespace DotNetTalk.CustomClaimProvider.EntraModel.Responses;

public class TokenIssuanceStartAction
{
    [JsonPropertyName("@odata.type")]
    public string ODataType { get; set; } = "microsoft.graph.tokenIssuanceStart.provideClaimsForToken";

    [JsonPropertyName("claims")]
    public CustomClaims Claims { get; set; }
}