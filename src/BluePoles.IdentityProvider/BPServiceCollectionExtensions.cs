﻿using MassTransit;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using SimpleIdServer.OAuth.Api.Authorization;
using SimpleIdServer.OAuth.Api.Authorization.ResponseTypes;
using SimpleIdServer.OAuth.Api.Authorization.Validators;
using SimpleIdServer.OAuth.Api.Configuration;
using SimpleIdServer.OAuth.Api.Register.Handlers;
using SimpleIdServer.OAuth.Api.Register.Validators;
using SimpleIdServer.OAuth.Api.Token.Handlers;
using SimpleIdServer.OAuth.Api.Token.TokenBuilders;
using SimpleIdServer.OAuth.Domains;
using SimpleIdServer.OAuth.Options;
using SimpleIdServer.OAuth.Persistence;
using SimpleIdServer.OpenID;
using SimpleIdServer.OpenID.Api.Authorization;
using SimpleIdServer.OpenID.Api.Authorization.ResponseTypes;
using SimpleIdServer.OpenID.Api.Authorization.Validators;
using SimpleIdServer.OpenID.Api.AuthSchemeProvider.Handlers;
using SimpleIdServer.OpenID.Api.BCAuthorize;
using SimpleIdServer.OpenID.Api.BCDeviceRegistration;
using SimpleIdServer.OpenID.Api.Configuration;
using SimpleIdServer.OpenID.Api.Register;
using SimpleIdServer.OpenID.Api.Token.Handlers;
using SimpleIdServer.OpenID.Api.Token.TokenBuilders;
using SimpleIdServer.OpenID.Domains;
using SimpleIdServer.OpenID.Helpers;
using SimpleIdServer.OpenID.Infrastructures.Jobs;
using SimpleIdServer.OpenID.Infrastructures.Locks;
using SimpleIdServer.OpenID.Jobs;
using SimpleIdServer.OpenID.Metadata;
using SimpleIdServer.OpenID.Options;
using SimpleIdServer.OpenID.Persistence;
using SimpleIdServer.OpenID.Persistence.InMemory;
using SimpleIdServer.OpenID.SubjectTypeBuilders;
using SimpleIdServer.OpenID.UI.Infrastructures;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BluePoles.IdentityProvider
{
    public static class BPServiceCollectionExtensions
    {
        /// <summary>
        /// Register OPENID dependencies.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static SimpleIdServerOpenIDBuilder AddCustomApi(this IServiceCollection services,
            Action<OpenIDHostOptions> openidOptions = null,
            Action<OAuthHostOptions> oauthOptions = null,
            Action<IBusRegistrationConfigurator> massTransitOptions = null)
        {
            var builder = new SimpleIdServerOpenIDBuilder(services);
            services.AddSIDOAuth();
            services
                .AddUI()
                .AddMetadata()
                .AddOpenIdConfiguration()
                .AddOpenIDStore()
                .AddOpenIDAuthorizationApi()
                .AddRegisterApi()
                .AddBCAuthorizeApi()
                .AddBCDeviceRegistrationApi()
                .AddOpenIDAuthentication()
                .AddManagementApi()
                //.AddBCAuthorizeJob()
                .AddInMemoryLock()
                .AddAuthSchemeProviderApi();
            if (openidOptions != null)
            {
                services.Configure(openidOptions);
            }
            else
            {
                services.Configure<OpenIDHostOptions>((opt) => { });
            }

            if (oauthOptions != null)
            {
                services.Configure(oauthOptions);
            }
            else
            {
                services.Configure<OAuthHostOptions>((opt) => { });
            }

            services.AddMassTransit(massTransitOptions != null ? massTransitOptions : (o) =>
            {
                o.UsingInMemory();
            });
            return builder;
        }

        private static IServiceCollection AddOpenIDAuthentication(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var openidHostOptions = serviceProvider.GetService<IOptionsMonitor<OpenIDHostOptions>>();
            services.AddAuthentication(openidHostOptions.CurrentValue.AuthenticationScheme)
                .AddCookie(openidHostOptions.CurrentValue.AuthenticationScheme, null, opts =>
                {
                    opts.Events.OnSigningIn += (ctx) =>
                    {
                        if (ctx.Principal != null && ctx.Principal.Identity != null && ctx.Principal.Identity.IsAuthenticated)
                        {
                            var nameIdentifier = ctx.Principal.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                            var ticket = new AuthenticationTicket(ctx.Principal, ctx.Properties, ctx.Scheme.Name);
                            var cookieValue = ctx.Options.TicketDataFormat.Protect(ticket, GetTlsTokenBinding(ctx));
                            ctx.Options.CookieManager.AppendResponseCookie(
                                ctx.HttpContext,
                                $"{ctx.Options.Cookie.Name}-{nameIdentifier}",
                                cookieValue,
                                ctx.CookieOptions);
                            return Task.CompletedTask;
                        }

                        return Task.CompletedTask;
                    };
                    opts.Events.OnSigningOut += (ctx) =>
                    {
                        if (ctx.HttpContext.User != null && ctx.HttpContext.User.Identity != null && ctx.HttpContext.User.Identity.IsAuthenticated)
                        {
                            var nameIdentifier = ctx.HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                            ctx.Options.CookieManager.DeleteCookie(
                                ctx.HttpContext,
                                $"{ctx.Options.Cookie.Name}-{nameIdentifier}",
                                ctx.CookieOptions);
                            return Task.CompletedTask;
                        }

                        return Task.CompletedTask;
                    };
                });
            return services;
        }

        private static IServiceCollection AddUI(this IServiceCollection services)
        {
            services.AddTransient<ISessionManager, SessionManager>();
            return services;
        }

        private static IServiceCollection AddMetadata(this IServiceCollection services)
        {
            services.AddTransient<IMetadataResultBuilder, MetadataResultBuilder>();
            return services;
        }

        private static IServiceCollection AddOpenIdConfiguration(this IServiceCollection services)
        {
            services.RemoveAll<IOAuthWorkflowConverter>();
            services.AddTransient<IOAuthWorkflowConverter, OpenIdWorkflowConverter>();
            return services;
        }

        private static IServiceCollection AddOpenIDStore(this IServiceCollection services)
        {
            var acrs = new List<AuthenticationContextClassReference>();
            var clients = new List<OpenIdClient>();
            var scopes = new List<OAuthScope>
            {
                SIDOpenIdConstants.StandardScopes.Address,
                SIDOpenIdConstants.StandardScopes.Email,
                SIDOpenIdConstants.StandardScopes.OfflineAccessScope,
                SIDOpenIdConstants.StandardScopes.OpenIdScope,
                SIDOpenIdConstants.StandardScopes.Phone,
                SIDOpenIdConstants.StandardScopes.Profile
            };
            var bcAuthorizeLst = new ConcurrentBag<BCAuthorize>();
            var authenticationSchemes = new List<SimpleIdServer.OpenID.Domains.AuthenticationSchemeProvider>();
            services.AddSingleton<IAuthenticationContextClassReferenceRepository>(new DefaultAuthenticationContextClassReferenceRepository(acrs));
            services.AddSingleton<IOAuthClientRepository>(new DefaultOpenIdClientRepository(clients));
            services.AddSingleton<IOAuthScopeRepository>(new DefaultOpenIdScopeRepository(scopes));
            services.AddSingleton<IBCAuthorizeRepository>(new DefaultBCAuthorizeRepository(bcAuthorizeLst));
            services.AddSingleton<IAuthenticationSchemeProviderRepository>(new DefaultAuthenticationSchemeProviderRepository(authenticationSchemes));
            return services;
        }

        private static IServiceCollection AddOpenIDAuthorizationApi(this IServiceCollection services)
        {
            services.RemoveAll<IAuthorizationRequestEnricher>();
            services.RemoveAll<IAuthorizationRequestValidator>();
            services.RemoveAll<IResponseTypeHandler>();
            services.RemoveAll<IAuthorizationRequestHandler>();
            services.RemoveAll<IUserConsentFetcher>();
            services.RemoveAll<ITokenBuilder>();
            services.AddTransient<IUserConsentFetcher, OpenIDUserConsentFetcher>();
            services.AddTransient<IAuthorizationRequestHandler, OpenIDAuthorizationRequestHandler>();
            services.AddTransient<IAuthorizationRequestValidator, OpenIDAuthorizationRequestValidator>();
            services.AddTransient<IResponseTypeHandler, SimpleIdServer.OpenID.Api.Authorization.ResponseTypes.AuthorizationCodeResponseTypeHandler>();
            services.AddTransient<IResponseTypeHandler, SimpleIdServer.OpenID.Api.Authorization.ResponseTypes.TokenResponseTypeHandler>();
            services.AddTransient<IResponseTypeHandler, IdTokenResponseTypeHandler>();
            services.AddTransient<ITokenBuilder, IdTokenBuilder>();
            services.AddTransient<ITokenBuilder, OpenIDAccessTokenBuilder>();
            services.AddTransient<ITokenBuilder, OpenIDRefreshTokenBuilder>();
            services.AddTransient<IClaimsJwsPayloadEnricher, ClaimsJwsPayloadEnricher>();
            services.AddTransient<IAuthorizationRequestEnricher, OpenIDAuthorizationRequestEnricher>();
            services.AddTransient<ISubjectTypeBuilder, PublicSubjectTypeBuilder>();
            services.AddTransient<ISubjectTypeBuilder, PairWiseSubjectTypeBuidler>();
            services.AddTransient<IAmrHelper, AmrHelper>();
            services.AddTransient<IExtractRequestHelper, ExtractRequestHelper>();
            return services;
        }

        private static IServiceCollection AddBCAuthorizeApi(this IServiceCollection services)
        {
            services.AddTransient<IGrantTypeHandler, CIBAHandler>();
            services.AddTransient<ICIBAGrantTypeValidator, CIBAGrantTypeValidator>();
            services.AddTransient<IBCAuthorizeHandler, BCAuthorizeHandler>();
            services.AddTransient<IBCAuthorizeRequestValidator, BCAuthorizeRequestValidator>();
            services.AddTransient<IBCNotificationService, BCNotificationService>();
            return services;
        }

        private static IServiceCollection AddBCDeviceRegistrationApi(this IServiceCollection services)
        {
            services.AddTransient<IBCDeviceRegistrationHandler, BCDeviceRegistrationHandler>();
            services.AddTransient<IBCDeviceRegistrationValidator, BCDeviceRegistrationValidator>();
            return services;
        }

        private static IServiceCollection AddBCAuthorizeJob(this IServiceCollection services)
        {
            services.AddHostedService<OpenIdServerHostedService>();
            services.AddTransient<IOpenIdJobServer, OpenIdJobServer>();
            services.AddTransient<IBCNotificationHandler, PingNotificationHandler>();
            services.AddTransient<IBCNotificationHandler, PushNotificationHandler>();
            services.AddTransient<IJob, BCNotificationJob>();
            return services;
        }

        private static IServiceCollection AddInMemoryLock(this IServiceCollection services)
        {
            services.AddSingleton<IDistributedLock, InMemoryDistributedLock>();
            return services;
        }

        private static IServiceCollection AddRegisterApi(this IServiceCollection services)
        {
            services.RemoveAll<IAddOAuthClientHandler>();
            services.RemoveAll<IGetOAuthClientHandler>();
            services.RemoveAll<IUpdateOAuthClientHandler>();
            services.RemoveAll<IOAuthClientValidator>();
            services.AddTransient<IAddOAuthClientHandler, AddOpenIdClientHandler>();
            services.AddTransient<IGetOAuthClientHandler, GetOpenIdClientHandler>();
            services.AddTransient<IUpdateOAuthClientHandler, UpdateOpenIdClientHandler>();
            services.AddTransient<IOAuthClientValidator, OpenIdClientValidator>();
            return services;
        }

        private static IServiceCollection AddManagementApi(this IServiceCollection services)
        {
            services.RemoveAll<SimpleIdServer.OAuth.Api.Management.Handlers.IGetOAuthClientHandler>();
            services.RemoveAll<SimpleIdServer.OAuth.Api.Management.Handlers.ISearchOauthClientsHandler>();
            services.RemoveAll<SimpleIdServer.OAuth.Api.Management.Handlers.IUpdateOAuthClientHandler>();
            services.AddTransient<SimpleIdServer.OAuth.Api.Management.Handlers.IGetOAuthClientHandler, SimpleIdServer.OpenID.Api.Management.GetOpenIdClientHandler>();
            services.AddTransient<SimpleIdServer.OAuth.Api.Management.Handlers.ISearchOauthClientsHandler, SimpleIdServer.OpenID.Api.Management.SearchOpenIdClientsHandler>();
            services.AddTransient<SimpleIdServer.OAuth.Api.Management.Handlers.IUpdateOAuthClientHandler, SimpleIdServer.OpenID.Api.Management.UpdateOpenIdClientHandler>();
            services.AddTransient<SimpleIdServer.OAuth.Api.Management.Handlers.IAddOAuthClientHandler, SimpleIdServer.OpenID.Api.Management.AddOpenIdClientHandler>();
            return services;
        }

        private static IServiceCollection AddAuthSchemeProviderApi(this IServiceCollection services)
        {
            services.AddTransient<IDisableAuthSchemeProviderHandler, DisableAuthSchemeProviderHandler>();
            services.AddTransient<IEnableAuthSchemeProviderHandler, EnableAuthSchemeProviderHandler>();
            services.AddTransient<IGetAllAuthSchemeProvidersHandler, GetAllAuthSchemeProvidersHandler>();
            services.AddTransient<IUpdateAuthSchemeProviderOptionsHandler, UpdateAuthSchemeProviderOptionsHandler>();
            services.AddTransient<IGetAuthSchemeProviderHandler, GetAuthSchemeProviderHandler>();
            return services;
        }

        private static string GetTlsTokenBinding(CookieSigningInContext context)
        {
            var binding = context.HttpContext.Features.Get<ITlsTokenBindingFeature>()?.GetProvidedTokenBindingId();
            return binding == null ? null : Convert.ToBase64String(binding);
        }
    }
}
