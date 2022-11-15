using BluePoles.IdentityProvider.OpenId.Management;
using BluePoles.IdentityProvider.Tests.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using SimpleIdServer.Common.Domains;
using SimpleIdServer.OAuth.Domains;
using SimpleIdServer.OAuth.Persistence;
using SimpleIdServer.OAuth.Persistence.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static MassTransit.ValidationResultExtensions;

namespace BluePoles.IdentityProvider.Tests
{
    [TestFixture]
    internal class TestManagement : BaseTest
    {
        [Test]
        public async Task TestGetClient()
        {
            var management = Services.GetRequiredService<Management>();

            var client = await management.GetClient("gatewayClient");

            Assert.IsTrue(client is not null);

            await Task.CompletedTask;
        }

        [Test]
        public async Task TestSearchClientWithParameters()
        {
            var management = Services.GetRequiredService<Management>();

            var clients = await management.Search<ResponseOAuthClient>(order_by: "client_id", take: 3);

            Assert.IsTrue(clients is not null);

            await Task.CompletedTask;
        }
        [Test]
        public async Task TestCreateClient()
        {
            var management = Services.GetRequiredService<Management>();

            var clientGuid = await management.CreateClient(new ResponseOAuthClient {
                ClientId = "aaaa",
                ClientSecret = "aaaaSecret"
            });

            Assert.That(clientGuid is not null);

            await Task.CompletedTask;
        }
        [Test]
        public async Task TestUpdateClient()
        {
            var management = Services.GetRequiredService<Management>();

            var result = await management.UpdateClient("21facaaf-8500-4f72-9ee8-207f7bed8017", new ResponseOAuthClient
            {
                ClientId = "newId",
                ClientSecret = "newSecret"
            });

            Assert.That(result, Is.EqualTo(HttpStatusCode.NoContent));

            await Task.CompletedTask;
        }
        [Test]
        public async Task TestCreateClientWithAttributes()
        {
            var management = Services.GetRequiredService<Management>();

            var result = await management.CreateClientWithAttributes(new ResponseOAuthClient
            {
                ClientNameFr = "leToni",
                ClientSecret = "aaaaSecret",
                ApplicationKind = 4,
                Scope = "manage_clients",
                ApplicationType = "web",
                ClientName = "theToni",
                GrantTypes = new List<string>() { "authorization_code", "client_credentials" }
                
            });

            Assert.That(result, Is.EqualTo(HttpStatusCode.NoContent));

            await Task.CompletedTask;
        }
        [Test]
        public async Task TestRegisterClient()
        {
            var management = Services.GetRequiredService<Management>();

            var client = await management.RegisterClient(new ResponseOAuthClient()
            {
                RedirectUris = new List<string> { "https://bluepoles.at/oauth" }
            });

            Assert.IsTrue(client is not null);
            await Task.CompletedTask;
        }
        //This test adds and then deletes a scope. (Probably not a good practice?)
        [Test]
        public async Task TestCreateAndDeleteScope()
        {
            var management = Services.GetRequiredService<Management>();

            string name = "newScope1";
            var scope = await management.CreateScope(name);

            Assert.IsTrue(scope is not null);

            var result = await management.DeleteScope(name);
            Assert.That(result, Is.EqualTo(HttpStatusCode.NoContent));

            await Task.CompletedTask;
        }

        [Test]
        public async Task TestUpdateScopeClaims()
        {
            var management = Services.GetRequiredService<Management>();

            var result = await management.UpdateScope(new ResponseOAuthScope
            {
                Name = "newScope2"
            ,
                Claims = new List<string>() { "profile", "picture", "gender" }
            });

            Assert.That(result, Is.EqualTo(HttpStatusCode.NoContent));

            await Task.CompletedTask;
        }

        [Test]
        public async Task TestGetScopes()
        {
            var management = Services.GetRequiredService<Management>();

            var scopes = await management.GetScopes();

            Assert.IsTrue(scopes is not null);

            await Task.CompletedTask;
        }

        [Test]
        public async Task TestGetScope()
        {
            var management = Services.GetRequiredService<Management>();

            var scopes = await management.GetScope("address");

            Assert.IsTrue(scopes is not null);

            await Task.CompletedTask;
        }

        [Test]
        public async Task TestSearchScopes()
        {
            var management = Services.GetRequiredService<Management>();

            var scopes = await management.Search<ResponseOAuthScope>();

            Assert.IsTrue(scopes is not null);

            await Task.CompletedTask;
        }
        
        [Test]
        public async Task TestUpdateUserPassword()
        {
            var management = Services.GetRequiredService<Management>();

            var result = await management.UpdateUserPassword("doctor", new ResponseOAuthUser { Password = "newPassword3"});

            Assert.That(result, Is.EqualTo(HttpStatusCode.NoContent));

            await Task.CompletedTask;
        }
        [Test]
        public async Task TestGetUserOneTimePassword()
        {
            var management = Services.GetRequiredService<Management>();

            var result = await management.GetUserOneTimePassword("sub");

            Assert.That(result is not null);

            await Task.CompletedTask;
        }

        [Test]
        public async Task TestDB() {
            var db = Services.GetRequiredService<IOAuthUserRepository>();
            var data = await db.FindOAuthUserByLogin("tes0tasdfasdfasdfasdfasd", new CancellationToken());
            
        }
        [Test]
        public async Task TestCreateOAuthUser() {
            var management = Services.GetRequiredService<Management>();
            var db = Services.GetRequiredService<IOAuthUserRepository>();
            string randomId = "Test" + Guid.NewGuid().ToString();
            DateTime dateTime = DateTime.Now;
            OAuthUser user = new OAuthUser()
            {
                //Setting a guid as the SCIMId for now, since SCIM is not implemented.
                Id = randomId,
                Consents = new List<OAuthConsent>()
                {
                    new OAuthConsent()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Claims = new List<string>()
                        {
                            "pictures",

                        }
                    },
                },
                OAuthUserClaims = new List<UserClaim>()
                {
                    new UserClaim("pictures", "png"),
                    new UserClaim("scim_id", randomId)
                },

                CreateDateTime = dateTime,
                UpdateDateTime = dateTime
                
            };
            await management.CreateOAuthUser(user);
            var clm = user.Claims;
        }
        
        [Test]
        public async Task TestUpdateOAuthUser() {
            var management = Services.GetRequiredService<Management>();
            var db = Services.GetRequiredService<IOAuthUserRepository>();
            string id = "Test28ee18eb-d7e0-4894-ab38-a48cf5d4e04e";
            DateTime dateTime = DateTime.Now;
            OAuthUser user = new OAuthUser()
            {
                Id = id,
                Consents = new List<OAuthConsent>()
                {
                    new OAuthConsent()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Claims = new List<string>()
                        {
                            "pictures",

                        }
                    },
                },
                OAuthUserClaims = new List<UserClaim>()
                {
                    new UserClaim("pictures", "png"),
                    new UserClaim("scim_id", id)
                },

                UpdateDateTime = dateTime
                
            };
            var result = await management.HandleOAuthUser(user);
            var clm = user.Claims;

            Assert.That(result, Is.EqualTo(HttpStatusCode.NoContent));
        }

        [Test]
        public async Task TestHandleOAuthUser()
        {
            var management = Services.GetRequiredService<Management>();
            var db = Services.GetRequiredService<IOAuthUserRepository>();
            string randomId = "Test" + Guid.NewGuid().ToString();
            DateTime dateTime = DateTime.Now;
            OAuthUser user = new OAuthUser()
            {
                //Setting a guid as the SCIMId for now, since SCIM is not implemented.
                Id = randomId,
                Consents = new List<OAuthConsent>()
                {
                    new OAuthConsent()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Claims = new List<string>()
                        {
                            "pictures",

                        }
                    },
                },
                OAuthUserClaims = new List<UserClaim>()
                {
                    new UserClaim("pictures", "png"),
                    new UserClaim("scim_id", randomId)
                },

                CreateDateTime = dateTime,
                UpdateDateTime = dateTime

            };
            await management.HandleOAuthUser(user);
            var clm = user.Claims;
        }

        [Test]
        public async Task TestGetUserByScimId()
        {
            var management = Services.GetRequiredService<Management>();

            var user = await management.GetOAuthUserByScimId("1");

            Assert.That(user is not null);

            await Task.CompletedTask;
        }

    }
}
