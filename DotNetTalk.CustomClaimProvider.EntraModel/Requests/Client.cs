using System.Text.Json.Serialization;

namespace DotNetTalk.CustomClaimProvider.EntraModel.Requests;

public class Client
{
    [JsonPropertyName("ip")]
    public string Ip { get; set; }

    [JsonPropertyName("locale")]
    public string Locale { get; set; }

    [JsonPropertyName("market")]
    public string Market { get; set; }
}
