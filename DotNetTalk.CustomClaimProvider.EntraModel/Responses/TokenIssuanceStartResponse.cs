using System.Text.Json.Serialization;

namespace DotNetTalk.CustomClaimProvider.EntraModel.Responses;

public class TokenIssuanceStartResponse
{
    [JsonPropertyName("data")]
    public TokenIssuanceStartResponseData Data { get; set; }
}