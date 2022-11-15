using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using RestSharp;
using static BluePoles.IdentityProvider.OpenId.Management.GetResponse;
using RestSharp.Authenticators;
using RestSharp.Authenticators.OAuth2;
using Microsoft.Extensions.Options;
using SimpleIdServer.OpenID.EF;
using Microsoft.EntityFrameworkCore;
using BluePoles.IdentityProvider.OpenId.Management;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var connectionString = builder.Configuration.GetConnectionString("OpenId");

builder.Services.AddOpenIDEF(opt =>
{
    opt.UseSqlServer(connectionString, opt =>
    {
        opt.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
    });

});
builder.Services.AddDbContext<OpenIdDBContext>(op =>
{
    op.UseSqlServer(connectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();


public class ClientCredationOpenIdData
{
    [JsonProperty("client_id")]
    public string ClientId { get; set; }
    [JsonProperty("client_id")]
    public string ClientSecret{ get; set; }
    
    public List<string> Scopes { get; set; }
}

//public class HttpC
//{
    

//    //Getting the Token
//    public static async Task<Response?> PostForToken()
//    {
//        BPOpenIDAuthenticator.
//        //var byteArray = new UTF8Encoding().GetBytes("<clientid>:<clientsecret>");
//        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Convert.ToBase64String(byteArray));
//        RestClient client = new RestClient("https://localhost:60000/");
//        string token = GetAccessToken();
//        client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(token, "Bearer")
//        var request = new RestRequest("/token");



//        var formData = new List<KeyValuePair<string, string>>();
//        formData.Add(new KeyValuePair<string, string>("client_id", "gatewayClient"));
//        formData.Add(new KeyValuePair<string, string>("client_secret", "gatewayClientPassword"));
//        formData.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
//        formData.Add(new KeyValuePair<string, string>("scope", "manage_clients manage_scopes manage_users query_scim_resource add_scim_resource delete_scim_resource update_scim_resource bulk_scim_resource scim_provision manage_authschemeproviders manage_relying_parties"));
//        formData.Add(new KeyValuePair<string, string>("response_type", "token"));

//        request.Content = new FormUrlEncodedContent(formData);
//        var clientCred = new ClientCredationOpenIdData()
//        {
//            ClientId = ""
//        };
//        var response = await client.SendAsync();

//        string jsonString = await response.Content.ReadAsStringAsync();
//        var jsonResponse = JsonConvert.DeserializeObject<Response>(jsonString);
//        return jsonResponse;

//    }

//    //Using the Token
//    public static async Task GetUsers()
//    {
//        var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:60002/Users");
//        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", PostForToken().Result.access_token);
        
//        client.DefaultRequestHeaders
//             .Accept
//             .Add(new MediaTypeWithQualityHeaderValue("application/json"));

//        var response = await client.SendAsync(request);

//        string jsonString = await response.Content.ReadAsStringAsync();
//        var jsonResponse = JsonConvert.DeserializeObject<Root>(jsonString);


        
//        Console.WriteLine(jsonResponse);
//        Console.ReadKey();
//    }
//}