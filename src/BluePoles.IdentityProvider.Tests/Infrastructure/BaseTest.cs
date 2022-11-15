using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using BluePoles.IdentityProvider.OpenId.Management;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using RestSharp;
using SimpleIdServer.OAuth.Persistence;
using SimpleIdServer.OpenID;
using SimpleIdServer.OpenID.EF;
using SimpleIdServer.OpenID.EF.Repositories;
using SimpleIdServer.OpenID.Persistence;
using BluePoles.IdentityProvider;

namespace BluePoles.IdentityProvider.Tests.Infrastructure
{
    public abstract class BaseTest
    {
        private IServiceScope TestScope;

        protected IServiceProvider Services => TestScope.ServiceProvider;

        private TestSettings? _settings;
        public TestSettings Settings => _settings ??= TestScope.ServiceProvider.GetRequiredService<TestSettings>();

        private ILogger? _logger;
        protected ILogger Logger => _logger ??= TestScope.ServiceProvider.GetRequiredService<ILogger>();

        public IMemoryCache? MemoryCache;
        public IWebHostEnvironment? WebHostEnvironment;

        protected IHost TestHost { get; set; }

        public BaseTest()
        {
            TestHost = new HostBuilder()
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder.AddJsonFile("appsettings.json");
                    builder.AddUserSecrets(Assembly.GetExecutingAssembly());
                    builder.AddJsonFile($"appsettings.{Environment.MachineName}.json", true);
                })
                .ConfigureServices((_env, services) =>
                {
                    var appSettings = _env.Configuration.GetSection("AppSettings").Get<TestSettings>();
                    //appSettings.SaveApiServicePath = $"{Directory.GetCurrentDirectory()}/App_Data/";
                    services.AddSingleton<IAppSettings, TestSettings>(e => appSettings);
                    services.AddSingleton<ILoggerFactory, LoggerFactory>();
                    services.Configure<BPOpenIDAuthenticatorOptions>(_env.Configuration.GetSection("AuthenticatorOptions"));
                    services.AddSingleton<BPOpenIDAuthenticator>();
                    services.AddOpenIDEF(opt =>
                     {
                         opt.UseSqlServer(_env.Configuration.GetConnectionString("OpenId"), opt =>
                         {
                             opt.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                         });

                     });
                    services.AddDbContext<OpenIdDBContext>(op =>
                    {
                        op.UseSqlServer(_env.Configuration.GetConnectionString("OpenId"));
                    });


                    services.AddSingleton<Management>();
                    
                })
                .ConfigureLogging((context, builder) =>
                {
                    //builder.AddNLog(context.Configuration.GetSection("Logging").Value);
                })
                //.UseNLog()
                .Build();
        }

        [SetUp]
        public virtual async Task SetUp()
        {
            TestScope = TestHost.Services.CreateScope();
            await Task.CompletedTask;
        }

        [TearDown]
        public virtual async Task TearDown()
        {
            TestScope.Dispose();
            await Task.CompletedTask;
        }

        [OneTimeSetUp]
        public virtual async Task OneTimeSetUp()
        {
            await Task.CompletedTask;
        }

        [OneTimeTearDown]
        public virtual async Task OneTimeTearDown()
        {

            await Task.CompletedTask;
        }
    }

    //public static class Nesto
    //{
    //    public static IServiceCollection AddOpenIDEF(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction = null)
    //    {
    //        services.AddDbContext<OpenIdDBContext>(optionsAction);
    //        services.AddTransient<IAuthenticationContextClassReferenceRepository, AuthenticationContextClassReferenceRepository>();
    //        services.AddTransient<IBCAuthorizeRepository, BCAuthorizeRepository>();
    //        services.AddTransient<IJsonWebKeyRepository, JsonWebKeyRepository>();
    //        services.AddTransient<IOAuthClientRepository, OAuthClientRepository>();
    //        services.AddTransient<IOAuthScopeRepository, OAuthScopeRepository>();
    //        services.AddTransient<IOAuthUserRepository, OAuthUserRepository>();
    //        services.AddTransient<ITokenRepository, TokenRepository>();
    //        services.AddTransient<ITranslationRepository, TranslationRepository>();
    //        services.AddTransient<IAuthenticationSchemeProviderRepository, AuthenticationSchemeProviderRepository>();
    //        return services;
    //    }
    //}
}

