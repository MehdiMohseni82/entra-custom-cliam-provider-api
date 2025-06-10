using System.Text.Json.Serialization;

namespace DotNetTalk.CustomClaimProvider.EntraModel.Responses;

public class TokenIssuanceStartResponseData
{
    [JsonPropertyName("@odata.type")]
    public string ODataType { get; set; } = "microsoft.graph.onTokenIssuanceStartResponseData";

    [JsonPropertyName("actions")]
    public List<TokenIssuanceStartAction> Actions { get; set; }
}