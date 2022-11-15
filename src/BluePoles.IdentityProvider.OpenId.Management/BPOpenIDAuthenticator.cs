using RestSharp;
using RestSharp.Authenticators;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Linq;
using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Text.Json;
using RestSharp.Serializers;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.JsonPatch.Operations;
using SimpleIdServer.Common.Domains;

namespace BluePoles.IdentityProvider.OpenId.Management
{
    record TokenResponse
    {
        [JsonPropertyName("token_type")]
        public string TokenType { get; init; }

        [JsonPropertyName("access_token")]
        public string AccessToken { get; init; }
    }

    public class BPOpenIDAuthenticatorOptions
    {
        public string BaseUrl { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }

    public class BPOpenIDAuthenticator : AuthenticatorBase
    {
        readonly string _baseUrl;
        readonly string _clientId;
        readonly string _clientSecret;
        public BPOpenIDAuthenticator(string baseUrl, string clientId, string clientSecret) : base(string.Empty)
        {
            _baseUrl = baseUrl;
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        public BPOpenIDAuthenticator(IOptions<BPOpenIDAuthenticatorOptions> options) : this(options.Value.BaseUrl, options.Value.ClientId, options.Value.ClientSecret)
        {
        }

        protected override async ValueTask<Parameter> GetAuthenticationParameter(string accessToken)
        {
            var token = string.IsNullOrEmpty(Token) ? await GetToken() : Token;
            return new HeaderParameter(KnownHeaders.Authorization, token);
        }

        public async Task<string> GetToken()
        {
            var options = new RestClientOptions(_baseUrl);
            using var client = new RestClient(options);

            var request = new RestRequest("/token", Method.Post)
            .AddParameter("client_id", _clientId)
            .AddParameter("client_secret", _clientSecret)
            .AddParameter("grant_type", "client_credentials")
            .AddParameter("scope", "manage_clients manage_scopes manage_users query_scim_resource add_scim_resource delete_scim_resource update_scim_resource bulk_scim_resource scim_provision manage_authschemeproviders manage_relying_parties");

            var response = await client.PostAsync<TokenResponse>(request);
            return $"{response!.TokenType} {response!.AccessToken}";
        }


    }
}