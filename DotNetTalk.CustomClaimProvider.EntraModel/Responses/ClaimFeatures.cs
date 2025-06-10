using System.Text.Json.Serialization;

namespace DotNetTalk.CustomClaimProvider.EntraModel.Responses;

public class ClaimFeatures
{
    [JsonPropertyName("_k")]
    public string Key { get; set; }

    [JsonPropertyName("_m")]
    public List<MetadataItem>? Metadata { get; set; }
}