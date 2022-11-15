using Newtonsoft.Json;
using SimpleIdServer.OAuth.Domains;
using System.Text.Json.Serialization;

namespace BluePoles.IdentityProvider.OpenId.Management
{
    public class Response
    {
        public double? expires_in { get; set; }
        public string? scope { get; set; }
        public string? refresh_token { get; set; }
        public string? access_token { get; set; }
        public string? token_type { get; set; }
    }

    public record ResponseOAuthClient
    {
        [JsonPropertyName("client_id")]
        public string ClientId { get; set; }

        [JsonPropertyName("grant_types")]
        public List<string> GrantTypes { get; set; }

        [JsonPropertyName("token_endpoint_auth_method")]
        public string TokenEndpointAuthMethod { get; set; }

        [JsonPropertyName("client_name")]
        public string ClientName { get; set; }

        [JsonPropertyName("client_name#fr")]
        public string ClientNameFr { get; set; }

        [JsonPropertyName("client_secret")]
        public string ClientSecret { get; set; }

        [JsonPropertyName("update_datetime")]
        public DateTime UpdateDatetime { get; set; }

        [JsonPropertyName("create_datetime")]
        public DateTime CreateDatetime { get; set; }

        [JsonPropertyName("client_id_issued_at")]
        public double ClientIdIssuedAt { get; set; }

        [JsonPropertyName("registration_client_uri")]
        public string RegistrationClientUri { get; set; }

        [JsonPropertyName("token_expiration_time_seconds")]
        public double TokenExpirationTimeSeconds { get; set; }

        [JsonPropertyName("refresh_token_expiration_time_seconds")]
        public double RefreshTokenExpirationTimeSeconds { get; set; }

        [JsonPropertyName("scope")]
        public string Scope { get; set; }

        [JsonPropertyName("require_auth_time")]
        public bool RequireAuthTime { get; set; }

        [JsonPropertyName("application_kind")]
        public int ApplicationKind { get; set; }
        [JsonPropertyName("redirect_uris")]
        public List<string> RedirectUris { get; set; }

        [JsonPropertyName("response_types")]
        public List<string> ResponseTypes { get; set; }

        [JsonPropertyName("application_type")]
        public string ApplicationType { get; set; }

        [JsonPropertyName("id_token_signed_response_alg")]
        public string IdTokenSignedResponseAlg { get; set; }

        [JsonPropertyName("post_logout_redirect_uris")]
        public List<string> PostLogoutRedirectUris { get; set; }
    }

    public record PagedResult<T>
    {
        [JsonPropertyName("start_index")]
        public int StartIndex { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("total_length")]
        public int TotalLength { get; set; }
        [JsonPropertyName("content")]
        public List<T> Content { get; set; }
    }

    public record ClientGUID
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
    }
    public record RegisterClientResponse
    {
        [JsonPropertyName("client_id")]
        public string ClientId { get; set; }

        [JsonPropertyName("registration_access_token")]
        public string RegistrationAccessToken { get; set; }

        [JsonPropertyName("grant_types")]
        public List<string> GrantTypes { get; set; }

        [JsonPropertyName("redirect_uris")]
        public List<string> RedirectUris { get; set; }

        [JsonPropertyName("token_endpoint_auth_method")]
        public string TokenEndpointAuthMethod { get; set; }

        [JsonPropertyName("response_types")]
        public List<string> ResponseTypes { get; set; }

        [JsonPropertyName("client_name")]
        public string ClientName { get; set; }

        [JsonPropertyName("client_secret")]
        public string ClientSecret { get; set; }

        [JsonPropertyName("update_datetime")]
        public DateTime UpdateDatetime { get; set; }

        [JsonPropertyName("create_datetime")]
        public DateTime CreateDatetime { get; set; }

        [JsonPropertyName("client_id_issued_at")]
        public double ClientIdIssuedAt { get; set; }

        [JsonPropertyName("registration_client_uri")]
        public string RegistrationClientUri { get; set; }

        [JsonPropertyName("token_expiration_time_seconds")]
        public double TokenExpirationTimeSeconds { get; set; }

        [JsonPropertyName("refresh_token_expiration_time_seconds")]
        public double RefreshTokenExpirationTimeSeconds { get; set; }

        [JsonPropertyName("scope")]
        public string Scope { get; set; }

        [JsonPropertyName("application_type")]
        public string ApplicationType { get; set; }

        [JsonPropertyName("subject_type")]
        public string SubjectType { get; set; }

        [JsonPropertyName("id_token_signed_response_alg")]
        public string IdTokenSignedResponseAlg { get; set; }

        [JsonPropertyName("require_auth_time")]
        public bool RequireAuthTime { get; set; }
    }

    public record ResponseOAuthScope
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("is_exposed")]
        public bool IsExposed { get; set; }

        [JsonPropertyName("is_standard")]
        public bool IsStandard { get; set; }

        [JsonPropertyName("update_datetime")]
        public DateTime UpdateDatetime { get; set; }

        [JsonPropertyName("create_datetime")]
        public DateTime CreateDatetime { get; set; }

        [JsonPropertyName("claims")]
        public List<string> Claims { get; set; }
    }

    public record ResponseOAuthUser
    {
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }

    public enum OrderByParameter
    {
        id,
        create_datetime,
        update_datetime
    }

    public record ResponseScimUser
    {

    }

    public class ResponseScimOAuthUser : OAuthUser
    {
        public ResponseScimOAuthUser(OAuthUser user, string scimId)
        {
            OAuthUserClaims = user.OAuthUserClaims;
            Consents = user.Consents;
            CreateDateTime = user.CreateDateTime;
            UpdateDateTime = user.UpdateDateTime;
            Credentials = user.Credentials;
            DeviceRegistrationToken = user.DeviceRegistrationToken;
            ExternalAuthProviders = user.ExternalAuthProviders;
            Id = user.Id;
            OTPCounter = user.OTPCounter;
            OTPKey = user.OTPKey;
            Sessions = user.Sessions;
            Status = user.Status;

            ScimId = scimId;
        }
        [JsonPropertyName("scim_id")]
        public string ScimId { get; set; }
    }
}
