using System.Text.Json.Serialization;

namespace DotNetTalk.CustomClaimProvider.EntraModel.Responses;

public class MetadataItem
{
    public MetadataItem(string key, string value)
    {
        Key = key;
        Value = value;
    }

    [JsonPropertyName("_k")]
    public string Key { get; set; }

    [JsonPropertyName("_vs")]
    public string Value { get; set; }
}