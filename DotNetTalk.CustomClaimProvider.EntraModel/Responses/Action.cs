using System.Text.Json.Serialization;

namespace DotNetTalk.CustomClaimProvider.EntraModel.Responses;

public class Action
{
    [JsonPropertyName("_v")]
    public Version Version => new Version(0, 1, 1);

    [JsonPropertyName("_df")]
    public List<ClaimFeatures> DisabledFeatures { get; set; }

    [JsonPropertyName("_m")]
    public GlobalMetadata? Metadata { get; set; }
}