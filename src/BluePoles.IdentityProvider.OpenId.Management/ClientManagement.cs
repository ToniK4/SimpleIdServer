using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace BluePoles.IdentityProvider.OpenId.Management
{
    public class ClientManagement
    {
        readonly string _baseUrl;
        //public async Task<JObject> GetClient()
        //{
        //    var options = new RestClientOptions(_baseUrl);
        //    using var client = new RestClient(options);

        //    var request = new RestRequest("/token", Method.Post)
        //    .AddParameter("client_id", _clientId)
        //    .AddParameter("client_secret", _clientSecret)
        //    .AddParameter("grant_type", "client_credentials")
        //    .AddParameter("scope", "manage_clients");

        //    var response = await client.PostAsync<TokenResponse>(request);
        //    return $"{response!.TokenType} {response!.AccessToken}";
        //}
    }
}
