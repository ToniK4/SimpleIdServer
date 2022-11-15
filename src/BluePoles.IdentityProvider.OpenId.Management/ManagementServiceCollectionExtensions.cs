using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleIdServer.OAuth.Persistence;
using SimpleIdServer.OpenID.EF.Repositories;
using SimpleIdServer.OpenID.EF;
using SimpleIdServer.OpenID.Persistence;

namespace BluePoles.IdentityProvider.OpenId.Management
{
    public static class ManagementServiceCollectionExtensions
    {
        public static IServiceCollection AddOpenIDEF(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction = null)
        {
            services.AddDbContext<OpenIdDBContext>(optionsAction);
            services.AddTransient<IAuthenticationContextClassReferenceRepository, AuthenticationContextClassReferenceRepository>();
            services.AddTransient<IBCAuthorizeRepository, BCAuthorizeRepository>();
            services.AddTransient<IJsonWebKeyRepository, JsonWebKeyRepository>();
            services.AddTransient<IOAuthClientRepository, OAuthClientRepository>();
            services.AddTransient<IOAuthScopeRepository, OAuthScopeRepository>();
            services.AddTransient<IOAuthUserRepository, OAuthUserRepository>();
            services.AddTransient<ITokenRepository, TokenRepository>();
            services.AddTransient<ITranslationRepository, TranslationRepository>();
            services.AddTransient<IAuthenticationSchemeProviderRepository, AuthenticationSchemeProviderRepository>();
            return services;
        }
    }
}
