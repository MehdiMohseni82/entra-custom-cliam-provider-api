using System.Text.Json.Serialization;

namespace DotNetTalk.CustomClaimProvider.EntraModel.Requests;

public class User
{
    [JsonPropertyName("displayName")]
    public string DisplayName { get; set; }

    [JsonPropertyName("givenName")]
    public string GivenName { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("mail")]
    public string Mail { get; set; }

    [JsonPropertyName("surname")]
    public string Surname { get; set; }

    [JsonPropertyName("userPrincipalName")]
    public string UserPrincipalName { get; set; }

    [JsonPropertyName("userType")]
    public string UserType { get; set; }

    [JsonPropertyName("preferredLanguage")]
    public string PreferredLanguage { get; set; }

    [JsonPropertyName("companyName")]
    public string CompanyName { get; set; }
}