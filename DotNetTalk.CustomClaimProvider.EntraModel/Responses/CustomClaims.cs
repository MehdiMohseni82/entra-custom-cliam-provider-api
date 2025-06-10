using System.Text.Json.Serialization;

namespace DotNetTalk.CustomClaimProvider.EntraModel.Responses;

public class CustomClaims
{
    [JsonPropertyName("metadata")]
    public string Metadata { get; set; }

    [JsonPropertyName("features")]
    public string Features { get; set; }
}