using System.Text.Json.Serialization;

namespace DotNetTalk.CustomClaimProvider.EntraModel.Responses;

public class MetadataSchema
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("defaultValue")]
    public string DefaultValue { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("key")]
    public string Key { get; set; }
}