using DotNetTalk.CustomClaimProvider.EntraModel.Requests;
using DotNetTalk.CustomClaimProvider.EntraModel.Responses;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace DotNetTalk.CustomClaimProvider.Functions.Functions.ClaimProviders;

/// <summary>
/// Azure Function for providing custom claims during Microsoft Entra ID token issuance.
/// This function is triggered by a TokenIssuanceStart HTTP POST request from Entra ID,
/// processes the input, and returns a response with custom claims to be injected into the token.
/// </summary>
/// <param name="logger">Logger for diagnostic and audit logging.</param>
public class CustomClaimProviderFunction(ILogger<CustomClaimProviderFunction> logger)
{
    private readonly string TenantId = "";
    private readonly string Audience = ""; // App ID URI
    private string Issuer => $"https://login.microsoftonline.com/{TenantId}/v2.0";

    /// <summary>
    /// HTTP-triggered Azure Function that is invoked by Microsoft Entra ID during token issuance.
    /// The function computes and returns custom claims to be injected into the issued token.
    /// </summary>
    /// <param name="request">HTTP POST request from Microsoft Entra containing user context.</param>
    /// <param name="executionContext">Function execution context.</param>
    /// <returns>HTTP response containing a valid `TokenIssuanceStartResponse` with custom claims.</returns>
    [Function("CustomClaimProviderFunction")]
    [OpenApiOperation(operationId: "ProvideCustomClaims", tags: ["Authentication", "CustomClaims"], Summary = "Provides custom claims for token issuance", Description = "Receives token issuance context from Entra ID and returns custom claims to enrich the token.")]
    [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(OnTokenIssuanceStartRequest), Required = true, Description = "The token issuance event data from Microsoft Entra ID.")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(TokenIssuanceStartResponse), Description = "Returns the custom claims to be injected into the token.")]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Description = "The request payload is invalid.")]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.InternalServerError, Description = "An unexpected error occurred during claim processing.")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "claims/custom")] HttpRequestData request, FunctionContext executionContext)
    {
        var stopwatch = Stopwatch.StartNew();

        if (!request.Headers.TryGetValues("Authorization", out var authHeaders))
            return request.CreateResponse(HttpStatusCode.Unauthorized);

        var bearerToken = authHeaders.FirstOrDefault()?.Replace("Bearer ", "");
        if (string.IsNullOrEmpty(bearerToken))
            return request.CreateResponse(HttpStatusCode.Unauthorized);

        var jwthandler = new JwtSecurityTokenHandler();
        var jwtToken = jwthandler.ReadJwtToken(bearerToken);

        var audienceClaim = string.Join(", ", jwtToken.Audiences);
        logger.LogError($"Audience(s) in JWT: {audienceClaim}");

        var validationParameters = new TokenValidationParameters
        {
            ValidAudience = Audience,
            ValidIssuer = Issuer,
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKeyResolver = (token, securityToken, kid, parameters) =>
            {
                var client = new HttpClient();
                var jwksUri = $"https://login.microsoftonline.com/{TenantId}/discovery/v2.0/keys";
                var keys = client.GetStringAsync(jwksUri).Result;
                var jsonWebKeySet = new JsonWebKeySet(keys);
                return jsonWebKeySet.Keys;
            }
        };

        try
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            jwtSecurityTokenHandler.ValidateToken(bearerToken, validationParameters, out var validatedToken);

            stopwatch.Stop();
            logger.LogInformation($"Token validation duration: {stopwatch.ElapsedMilliseconds} ms");
        }
        catch (Exception ex)
        {
            logger.LogError($"JWT validation error: {ex.Message}");
            return request.CreateResponse(HttpStatusCode.Unauthorized);
        }

        var requestObject = request.ReadFromJsonAsync<OnTokenIssuanceStartRequest>().Result;
        // requestObject.Data.AuthenticationContext.User.Id <= User Id come here.
        
        logger.LogInformation($"{nameof(CustomClaimProviderFunction)} has been called.");
        
        var response = request.CreateResponse(statusCode: HttpStatusCode.OK);
        var finalResponse = new TokenIssuanceStartResponse
        {
            Data = new TokenIssuanceStartResponseData
            {
                Actions =
                [
                    new TokenIssuanceStartAction
                    {
                        Claims = new CustomClaims
                        {
                            Metadata = "Test Meta Data", // Custom Claim come here.
                            Features = "Test Feature"
                        }
                    }
                ]
            }
        };

        await response.WriteAsJsonAsync(finalResponse);
        return response;
    }
}
