using System.Text.Json.Serialization;

namespace DotNetTalk.CustomClaimProvider.EntraModel.Responses;

public class FeatureSchema
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("key")]
    public string Key { get; set; }

    [JsonPropertyName("isUsed")]
    public bool IsUsed { get; set; }

    [JsonPropertyName("metadata")]
    public List<MetadataSchema> Metadata { get; set; }
}