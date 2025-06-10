using System.Text.Json.Serialization;

namespace DotNetTalk.CustomClaimProvider.EntraModel.Responses;

public class GlobalMetadata
{
    [JsonPropertyName("_li")]
    public string LicenseId { get; set; }

    [JsonPropertyName("_orgId")]
    public string OrganizationId { get; set; }

    [JsonPropertyName("_aa")]
    public long? ActivatedAt { get; set; }

    [JsonPropertyName("_ea")]
    public long? ExpiredAt { get; set; }

    [JsonPropertyName("_na")]
    public long? NextAvailableLicense { get; set; }

    [JsonPropertyName("_lt")]
    public byte? LicenseType { get; set; }

    [JsonPropertyName("_sk")]
    public byte? CanUseWithoutLicense { get; set; }

    [JsonPropertyName("_err")]
    public byte? Error { get; set; }
}