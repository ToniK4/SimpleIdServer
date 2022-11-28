using RestSharp;
using RestSharp.Authenticators;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using SimpleIdServer.OAuth.Persistence.Parameters;
using System.Net;
using SimpleIdServer.OpenID.EF;
using SimpleIdServer.OAuth.Persistence;
using SimpleIdServer.OAuth.Domains;
using static BluePoles.IdentityProvider.OpenId.Management.GetResponse;

namespace BluePoles.IdentityProvider.OpenId.Management
{
    public class Management
    {
        //private IServiceScope serviceScope;
        protected IServiceProvider Services;
        protected RestClient Client { get; init; }
        protected IAuthenticator Auth { get; init; }
        protected OpenIdDBContext DBContext { get; init; }

        public Management(IOptions<BPOpenIDAuthenticatorOptions> options, BPOpenIDAuthenticator auth, OpenIdDBContext context, IServiceProvider serviceProvider)
        {
            Client = new RestClient(new RestClientOptions(options.Value.BaseUrl));
            Client.Authenticator = auth;
            DBContext = context;
            Services = serviceProvider;
        }

        public async Task<ResponseOAuthClient> GetClient(string id)
        {
            var request = new RestRequest($"management/clients/{id}", Method.Get);

            var response = await Client.GetAsync<ResponseOAuthClient>(request);

            return response;
        }


        //Pogledaj switch

        /// <summary>
        /// Typed OAuth resource search method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="skip">Index at which the query starts.</param>
        /// <param name="take">Number of elements the query returns.</param>
        /// <param name="order_by">Options: "id", "create_datetime", "update_datetime"</param>
        /// <param name="order">0 = ASCENDING, 1 = DESCENDING</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<PagedResult<T>> Search<T>(int? skip = null, int? take = null, string? order_by = null, SortOrders? order = 0) => typeof(T).Name switch
        {
            nameof(ResponseOAuthScope) => await Search<T>("scopes", skip, take, order_by, order),
            nameof(ResponseOAuthClient) => await Search<T>("clients", skip, take, order_by, order),
            _ => throw new ArgumentException($"Invalid type {typeof(T)}"),
        };

        private async Task<PagedResult<T>> Search<T>(string urlSegment, int? skip = null, int? take = null, string? order_by = null, SortOrders? order = 0)
        {
            var request = new RestRequest($"management/{urlSegment}/.search", Method.Get);
            request
                .AddQueryParameter("start_index", skip.ToString())
                .AddQueryParameter("count", take.ToString())
                .AddQueryParameter(nameof(order_by), order_by)
                .AddQueryParameter(nameof(order), order.ToString());
            var response = await Client.GetAsync<PagedResult<T>>(request);

            return response;
        }

        //The management/clients endpoint should not be used by new clients, that's what the register endpoint is for.
        public async Task<ClientGUID> CreateClient(ResponseOAuthClient client)
        {
            var request = new RestRequest($"management/clients", Method.Post);
            request.AddJsonBody(client);
            var response = await Client.PostAsync<ClientGUID>(request);

            return response;
        }

        public async Task<HttpStatusCode> UpdateClient(string id, ResponseOAuthClient client)
        {
            var request = new RestRequest($"management/clients/{id}", Method.Put);
            request.AddJsonBody(client);
            var response = await Client.PutAsync(request);
            return response.StatusCode;
        }
        public async Task<HttpStatusCode> CreateClientWithAttributes(ResponseOAuthClient client)
        {
            ClientGUID client_guid = CreateClient(client).Result;
            if (client_guid == null)
            {
                throw new ArgumentNullException("Client GUID");
            }

            var request = new RestRequest($"management/clients/{client_guid.Id}", Method.Put);
            request.AddJsonBody(client);
            var response = await Client.PutAsync(request);
            var nesto = response;
            return response.StatusCode;
        }
        public async Task<HttpStatusCode> DeleteClient(string name)
        {
            var request = new RestRequest($"management/Clients/{name}", Method.Delete);

            var response = await Client.DeleteAsync(request);
            return response.StatusCode;
        }

        //TODO: replace argument with typed class.
        public async Task<RegisterClientResponse?> RegisterClient(ResponseOAuthClient client)
        {
            var request = new RestRequest($"register", Method.Post);


            request.AddJsonBody(client);

            var response = await Client.PostAsync<RegisterClientResponse>(request);
            return response;
        }

        public async Task<ResponseOAuthScope> CreateScope(string name)
        {
            var request = new RestRequest($"management/scopes", Method.Post);

            ResponseOAuthScope scope = new ResponseOAuthScope();
            scope.Name = name;

            request.AddJsonBody(scope);
            var response = await Client.PostAsync(request);
            scope.Name = response.Content;
            return scope;
        }


        //The argument is an OAuthScope but the rest method only updates the claims property.
        public async Task<HttpStatusCode> UpdateScope(ResponseOAuthScope scope)
        {
            var request = new RestRequest($"management/scopes/{scope.Name}", Method.Put);

            request.AddJsonBody(scope);

            var response = await Client.PutAsync(request);

            return response.StatusCode;
        }

        public async Task<List<ResponseOAuthScope>> GetScopes()
        {
            var request = new RestRequest($"management/scopes", Method.Get);

            var response = await Client.GetAsync<List<ResponseOAuthScope>>(request);
            return response;
        }

        public async Task<ResponseOAuthScope> GetScope(string id)
        {
            var request = new RestRequest($"management/scopes/{id}", Method.Get);

            var response = await Client.GetAsync<ResponseOAuthScope>(request);
            return response;
        }

        public async Task<HttpStatusCode> DeleteScope(string name)
        {
            var request = new RestRequest($"management/scopes/{name}", Method.Delete);

            var response = await Client.DeleteAsync(request);
            return response.StatusCode;
        }


        //Not done
        public async Task<NoContentResult?> CreateUserByScimId()
        {
            var request = new RestRequest($"management/users/scim", Method.Post);


            var response = await Client.PostAsync<NoContentResult>(request);
            throw new NotImplementedException();
        }

        public async Task<HttpStatusCode> UpdateUserPassword(string id, ResponseOAuthUser user)
        {
            var request = new RestRequest($"management/users/{id}/password", Method.Put);
            request.AddJsonBody(user);

            var response = await Client.PutAsync(request);
            return response.StatusCode;
        }
        public async Task<string> GetUserOneTimePassword(string id)
        {
            var request = new RestRequest($"management/users/{id}/otp", Method.Get);

            var response = await Client.GetAsync(request);
            return response.Content;
        }

        //public async Task CreateOAuthUser(OAuthUser user)
        //{
        //    CancellationToken cancellationToken = new CancellationToken();
        //    var db = Services.GetRequiredService<IOAuthUserRepository>();
        //    var data = await db.Add(user, cancellationToken);
        //    await db.SaveChanges(cancellationToken);
        //}

        //public async Task GetOAuthUser(string id)
        //{
        //    CancellationToken cancellationToken = new CancellationToken();
        //    var db = Services.GetRequiredService<IOAuthUserRepository>();
        //    var data = await db.FindOAuthUserByClaim("scim_id", id, cancellationToken);
        //    await db.SaveChanges(cancellationToken);
        //}
        
        public async Task CreateOAuthUser(OAuthUser user)
        {
            CancellationToken cancellationToken = new CancellationToken();
            var db = Services.GetRequiredService<IOAuthUserRepository>();
            var data = await db.Add(user, cancellationToken);
            await db.SaveChanges(cancellationToken);
        }

        public async Task<OAuthUser> GetOAuthUserByScimId(string id)
        {
            var request = new RestRequest($"management/users/scim/{id}", Method.Get);

            var response = await Client.GetAsync<OAuthUser>(request);
            return response;
        }

        public async Task<HttpStatusCode> HandleOAuthUser(OAuthUser user)
        {
            var request = new RestRequest($"management/users/scim", Method.Post);
            ResponseScimOAuthUser scimUser = new ResponseScimOAuthUser(user,user.Id);
            scimUser.ScimId = user.Id;
            request.AddJsonBody(scimUser);
            var response = await Client.PostAsync(request);
            return response.StatusCode;
        }

    }
}