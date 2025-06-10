using System.Text.Json.Serialization;

namespace DotNetTalk.CustomClaimProvider.EntraModel.Requests;

public class OnTokenIssuanceStartRequest
{
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("source")]
    public string Source { get; set; }

    [JsonPropertyName("data")]
    public OnTokenIssuanceStartData Data { get; set; }
}