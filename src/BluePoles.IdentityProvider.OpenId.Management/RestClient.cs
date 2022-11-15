using RestSharp;
using BluePoles.IdentityProvider.OpenId.Management;

namespace BluePoles.IdentityProvider.OpenId.Management
{
    //public class BPRestClient : IBPRestClient, IDisposable
    //{
    //    readonly RestClient _client;

    //    public BPRestClient(string apiKey, string apiKeySecret)
    //    {
    //        var options = new RestClientOptions("https://localhost:60002");

    //        _client = new RestClient(options)
    //        {
    //            Authenticator = BPOpenIDAuthenticator.GetInstance()
    //        };
    //    }

    //    public async Task<Users> GetUsers()
    //    {
    //        var response = await _client.GetJsonAsync<UsersSingleObject<Users>>(
    //            "/Users"
    //        );
    //        return response!.Data;
    //    }

    //    record UsersSingleObject<T>(T Data);

    //    public void Dispose()
    //    {
    //        _client?.Dispose();
    //        GC.SuppressFinalize(this);
    //    }
    //}
}
