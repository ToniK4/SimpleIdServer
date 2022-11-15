﻿// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Microsoft.AspNetCore.Authentication.Facebook;
using Newtonsoft.Json;
using SimpleIdServer.Common.Domains;
using SimpleIdServer.Common.Helpers;
using SimpleIdServer.Jwt;
using SimpleIdServer.OAuth.Domains;
using SimpleIdServer.OpenID.Domains;
using SimpleIdServer.Saml.Sp;
using System;
using System.Collections.Generic;
using SimpleIdServer.OpenID;
using BluePoles.IdentityProvider.Converters;

namespace BluePoles.IdentityProvider
{
    public class OpenIdDefaultConfiguration
    {
        public static OAuthScope AccountsScope = new OAuthScope
        {
            Name = "accounts",
            IsExposedInConfigurationEdp = true,
            IsStandardScope = true,
            CreateDateTime = DateTime.UtcNow,
            UpdateDateTime = DateTime.UtcNow
        };
        private static OAuthScope ManageClientsScope = new OAuthScope
        {
            Name = "manage_clients",
            IsExposedInConfigurationEdp = true,
            IsStandardScope = false,
            CreateDateTime = DateTime.UtcNow,
            UpdateDateTime = DateTime.UtcNow
        };
        private static OAuthScope ManageScopesScope = new OAuthScope
        {
            Name = "manage_scopes",
            IsExposedInConfigurationEdp = true,
            IsStandardScope = false,
            CreateDateTime = DateTime.UtcNow,
            UpdateDateTime = DateTime.UtcNow
        };
        private static OAuthScope QueryScimResource = new OAuthScope
        {
            Name = "query_scim_resource",
            IsExposedInConfigurationEdp = true,
            IsStandardScope = false,
            CreateDateTime = DateTime.UtcNow,
            UpdateDateTime = DateTime.UtcNow
        };
        private static OAuthScope AddScimResource = new OAuthScope
        {
            Name = "add_scim_resource",
            IsExposedInConfigurationEdp = true,
            IsStandardScope = false,
            CreateDateTime = DateTime.UtcNow,
            UpdateDateTime = DateTime.UtcNow
        };
        private static OAuthScope DeleteScimResource = new OAuthScope
        {
            Name = "delete_scim_resource",
            IsExposedInConfigurationEdp = true,
            IsStandardScope = false,
            CreateDateTime = DateTime.UtcNow,
            UpdateDateTime = DateTime.UtcNow
        };
        private static OAuthScope UpdateScimResource = new OAuthScope
        {
            Name = "update_scim_resource",
            IsExposedInConfigurationEdp = true,
            IsStandardScope = false,
            CreateDateTime = DateTime.UtcNow,
            UpdateDateTime = DateTime.UtcNow
        };
        private static OAuthScope BulkScimResource = new OAuthScope
        {
            Name = "bulk_scim_resource",
            IsExposedInConfigurationEdp = true,
            IsStandardScope = false,
            CreateDateTime = DateTime.UtcNow,
            UpdateDateTime = DateTime.UtcNow
        };
        private static OAuthScope ScimProvision = new OAuthScope
        {
            Name = "scim_provision",
            IsExposedInConfigurationEdp = true,
            IsStandardScope = false,
            CreateDateTime = DateTime.UtcNow,
            UpdateDateTime = DateTime.UtcNow
        };
        private static OAuthScope ManageUsers = new OAuthScope
        {
            Name = "manage_users",
            IsExposedInConfigurationEdp = true,
            IsStandardScope = false,
            CreateDateTime = DateTime.UtcNow,
            UpdateDateTime = DateTime.UtcNow
        };
        private static OAuthScope ManageHumanTaskInstance = new OAuthScope
        {
            Name = "manage_humantaskinstance",
            IsExposedInConfigurationEdp = true,
            IsStandardScope = false,
            CreateDateTime = DateTime.UtcNow,
            UpdateDateTime = DateTime.UtcNow
        };
        private static OAuthScope ManageAuthSchemeProviders = new OAuthScope
        {
            Name = "manage_authschemeproviders",
            IsExposedInConfigurationEdp = true,
            IsStandardScope = false,
            CreateDateTime = DateTime.UtcNow,
            UpdateDateTime = DateTime.UtcNow
        };
        private static OAuthScope ManageRelyingParties = new OAuthScope
        {
            Name = "manage_relying_parties",
            IsExposedInConfigurationEdp = true,
            IsStandardScope = false,
            CreateDateTime = DateTime.UtcNow,
            UpdateDateTime = DateTime.UtcNow
        };
        public static List<OAuthScope> Scopes = new List<OAuthScope>
        {
            SIDOpenIdConstants.StandardScopes.OpenIdScope,
            SIDOpenIdConstants.StandardScopes.Phone,
            SIDOpenIdConstants.StandardScopes.Profile,
            SIDOpenIdConstants.StandardScopes.Role,
            SIDOpenIdConstants.StandardScopes.OfflineAccessScope,
            SIDOpenIdConstants.StandardScopes.Email,
            SIDOpenIdConstants.StandardScopes.Address,
            SIDOpenIdConstants.StandardScopes.ScimScope,
            AccountsScope,
            ManageClientsScope,
            ManageScopesScope,
            QueryScimResource,
            AddScimResource,
            DeleteScimResource,
            UpdateScimResource,
            BulkScimResource,
            ScimProvision,
            ManageUsers,
            ManageAuthSchemeProviders,
            ManageRelyingParties
        };

        public static List<AuthenticationSchemeProvider> GetAuthenticationProviderSchemes(string baseSamlIdpUrl)
        {
            return new List<AuthenticationSchemeProvider>
            {
                new AuthenticationSchemeProvider
                {
                    CreateDateTime = DateTime.UtcNow,
                    DisplayName = "Facebook",
                    HandlerFullQualifiedName = typeof(FacebookHandler).AssemblyQualifiedName,
                    Id = Guid.NewGuid().ToString(),
                    Name = "Facebook",
                    Options = JsonConvert.SerializeObject(new FacebookOptionsLite
                    {
                        AppId = "2130151727301440",
                        AppSecret = "fbd2fe5e37602f333908eff93eae295d"
                    }),
                    JsonConverter = typeof(FacebookOptionsJsonConverter).AssemblyQualifiedName,
                    OptionsFullQualifiedName = typeof(FacebookOptionsLite).AssemblyQualifiedName,
                    PostConfigureOptionsFullQualifiedName = typeof(FacebookPostConfigureOptions).AssemblyQualifiedName,
                    UpdateDateTime = DateTime.UtcNow,
                    IsEnabled = true
                },
                new AuthenticationSchemeProvider
                {
                    CreateDateTime = DateTime.UtcNow,
                    DisplayName = "Saml",
                    HandlerFullQualifiedName = typeof(SamlSpHandler).AssemblyQualifiedName,
                    Id = Guid.NewGuid().ToString(),
                    Name = SamlSpDefaults.AuthenticationScheme,
                    Options = JsonConvert.SerializeObject(new SamlSpOptionsLite
                    {
                        IdpMetadataUrl = $"{baseSamlIdpUrl}/saml/metadata",
                        SPId = "urn:idp",
                        SPName = "IdentityServer"
                    }),
                    JsonConverter = typeof(SamlSpOptionsJsonConverter).AssemblyQualifiedName,
                    OptionsFullQualifiedName = typeof(SamlSpOptionsLite).AssemblyQualifiedName,
                    PostConfigureOptionsFullQualifiedName = typeof(SamlSpPostConfigureOptions).AssemblyQualifiedName,
                    UpdateDateTime = DateTime.UtcNow,
                    IsEnabled = true
                }
            };
        }

        public static List<AuthenticationContextClassReference> AcrLst => new List<AuthenticationContextClassReference>
        {
            new AuthenticationContextClassReference
            {
                DisplayName = "First level of assurance",
                Name = "sid-load-01",
                AuthenticationMethodReferences = new List<string>
                {
                    "pwd"
                }
            },
            new AuthenticationContextClassReference
            {
                DisplayName = "Second level of assurance",
                Name = "sid-load-02",
                AuthenticationMethodReferences = new List<string>
                {
                    "pwd",
                    "sms"
                }
            },
            new AuthenticationContextClassReference
            {
                DisplayName = "Second level of assurance (email)",
                Name = "sid-load-021",
                AuthenticationMethodReferences = new List<string>
                {
                    "email"
                }
            }
        };

        public static List<OAuthUser> Users => new List<OAuthUser>
        {
            new OAuthUser
            {
                Id = "sub",
                Credentials = new List<UserCredential>
                {
                    new UserCredential
                    {
                        CredentialType = "pwd",
                        Value = PasswordHelper.ComputeHash("password")
                    }
                },
                CreateDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now,
                OTPKey = "HQI6X6V2MEP44J4NLZJ65VKAHCSSCNFL",
                DeviceRegistrationToken = "ciyortoPQHGluxo-vIZLu7:APA91bHRrB-mdgHl6IQFu4XNWR5VBXxOjaq-gAAuxCzswQAGeryvFaBqoJqJN_oSEtPZMTknRe2rixJj5cjnaWkCin8NSXm7Gug6peZd9EpJgJ98CNHqOudcFv_h3jp4dpgWn6imb7sR",
                OAuthUserClaims = new List<UserClaim>
                {
                    new UserClaim(Constants.UserClaims.Subject, "sub"),
                    new UserClaim(Constants.UserClaims.Name, "name"),
                    new UserClaim(Constants.UserClaims.FamilyName, "familyName"),
                    new UserClaim(Constants.UserClaims.UniqueName, "uniquename"),
                    new UserClaim(Constants.UserClaims.GivenName, "givenName"),
                    new UserClaim(Constants.UserClaims.MiddleName, "middleName"),
                    new UserClaim(Constants.UserClaims.NickName, "nickName"),
                    new UserClaim(Constants.UserClaims.BirthDate, "07-10-1989"),
                    new UserClaim(Constants.UserClaims.PreferredUserName, "preferredUserName"),
                    new UserClaim(Constants.UserClaims.ZoneInfo, "zoneInfo"),
                    new UserClaim(Constants.UserClaims.Locale, "locale"),
                    new UserClaim(Constants.UserClaims.Picture, "picture"),
                    new UserClaim(Constants.UserClaims.WebSite, "website"),
                    new UserClaim(Constants.UserClaims.Profile, "profile"),
                    new UserClaim(Constants.UserClaims.Gender, "gender"),
                    new UserClaim(Constants.UserClaims.Email, "agentsimpleidserver@gmail.com"),
                    new UserClaim(Constants.UserClaims.UpdatedAt, "1612355959", ClaimValueTypes.INTEGER),
                    new UserClaim(Constants.UserClaims.EmailVerified, "true", ClaimValueTypes.BOOLEAN),
                    new UserClaim(Constants.UserClaims.Address, "{ 'street_address': '1234 Hollywood Blvd.', 'locality': 'Los Angeles', 'region': 'CA', 'postal_code': '90210', 'country': 'US' }", ClaimValueTypes.JSONOBJECT),
                    new UserClaim(Constants.UserClaims.PhoneNumber, "+1 (310) 123-4567"),
                    new UserClaim(Constants.UserClaims.PhoneNumberVerified, "true", ClaimValueTypes.BOOLEAN),
                    new UserClaim(Constants.UserClaims.Role, "visitor")
                }
            },
            new OAuthUser
            {
                Id = "administrator",
                Credentials = new List<UserCredential>
                {
                    new UserCredential
                    {
                        CredentialType = "pwd",
                        Value = PasswordHelper.ComputeHash("password")
                    }
                },
                OAuthUserClaims = new List<UserClaim>
                {
                    new UserClaim(Constants.UserClaims.Subject, "administrator"),
                    new UserClaim(Constants.UserClaims.GivenName, "administrator"),
                    new UserClaim(Constants.UserClaims.Role, "admin")
                }
            },
            new OAuthUser
            {
                Id = "businessanalyst",
                Credentials = new List<UserCredential>
                {
                    new UserCredential
                    {
                        CredentialType = "pwd",
                        Value = PasswordHelper.ComputeHash("password")
                    }
                },
                OAuthUserClaims = new List<UserClaim>
                {
                    new UserClaim(Constants.UserClaims.Subject, "businessanalyst"),
                    new UserClaim(Constants.UserClaims.GivenName, "businessanalyst"),
                    new UserClaim(Constants.UserClaims.Role, "businessanalyst"),
                    new UserClaim(Constants.UserClaims.Role, "visitor")
                }
            },
            new OAuthUser
            {
                Id = "caseworker",
                Credentials = new List<UserCredential>
                {
                    new UserCredential
                    {
                        CredentialType = "pwd",
                        Value = PasswordHelper.ComputeHash("password")
                    }
                },
                OAuthUserClaims = new List<UserClaim>
                {
                    new UserClaim(Constants.UserClaims.Subject, "caseworker"),
                    new UserClaim(Constants.UserClaims.GivenName, "caseworker"),
                    new UserClaim(Constants.UserClaims.Role, "caseworker")
                }
            },
            new OAuthUser
            {
                Id = "scimUser",
                Credentials = new List<UserCredential>
                {
                    new UserCredential
                    {
                        CredentialType = "pwd",
                        Value = PasswordHelper.ComputeHash("password")
                    }
                },
                OAuthUserClaims = new List<UserClaim>
                {
                    new UserClaim(Constants.UserClaims.Subject, "scimUser"),
                    new UserClaim("scim_id", "1")
                }
            },
            new OAuthUser
            {
                Id = "umaUser",
                Credentials = new List<UserCredential>
                {
                    new UserCredential
                    {
                        CredentialType = "pwd",
                        Value = PasswordHelper.ComputeHash("password")
                    }
                },
                OAuthUserClaims = new List<UserClaim>
                {
                    new UserClaim(Constants.UserClaims.Subject, "umaUser"),
                    new UserClaim(Constants.UserClaims.Name, "User"),
                    new UserClaim(Constants.UserClaims.UniqueName, "User")
                }
            },
            new OAuthUser
            {
                Id = "doctor",
                Credentials = new List<UserCredential>
                {
                    new UserCredential
                    {
                        CredentialType = "pwd",
                        Value = PasswordHelper.ComputeHash("password")
                    }
                },
                OAuthUserClaims = new List<UserClaim>
                {
                    new UserClaim(Constants.UserClaims.Subject, "doctor"),
                    new UserClaim(Constants.UserClaims.Name, "Doctor"),
                    new UserClaim(Constants.UserClaims.GivenName, "Doctor"),
                    new UserClaim(Constants.UserClaims.UniqueName, "Doctor")
                }
            }
        };

        public static List<OpenIdClient> GetClients(JsonWebKey firstMtlsClientJsonWebKey, JsonWebKey secondMtlsClientJsonWebKey, JsonWebKey jsonWebKey)
        {
            return new List<OpenIdClient>
            {
                new OpenIdClient
                {
                    ClientId = "newsAggregatorWebsite",
                    ClientSecret = "newsAggregatorWebsiteSecret",
                    ApplicationKind = ApplicationKinds.SPA,
                    TokenEndPointAuthMethod = "pkce",
                    ApplicationType = "web",
                    UpdateDateTime = DateTime.UtcNow,
                    CreateDateTime = DateTime.UtcNow,
                    TokenExpirationTimeInSeconds = 60 * 30,
                    RefreshTokenExpirationTimeInSeconds = 60 * 30,
                    TokenSignedResponseAlg = "RS256",
                    IdTokenSignedResponseAlg = "RS256",
                    AllowedScopes = new List<OAuthScope>
                    {
                        SIDOpenIdConstants.StandardScopes.OpenIdScope,
                        SIDOpenIdConstants.StandardScopes.Profile,
                        SIDOpenIdConstants.StandardScopes.Email,
                        SIDOpenIdConstants.StandardScopes.Role
                    },
                    GrantTypes = new List<string>
                    {
                        "authorization_code"
                    },
                    RedirectionUrls = new List<string>
                    {
                        "http://localhost:4200",
                        "http://localhost:4200/silent-refresh.html"
                    },
                    PreferredTokenProfile = "Bearer",
                    ResponseTypes = new List<string>
                    {
                        "code"
                    }
                },
                new OpenIdClient
                {
                    ClientId = "firstMtlsClient",
                    ClientSecret = "mtsClientSecret",
                    ApplicationKind = ApplicationKinds.Web,
                    ApplicationType = "web",
                    UpdateDateTime = DateTime.UtcNow,
                    CreateDateTime = DateTime.UtcNow,
                    TokenExpirationTimeInSeconds = 60 * 30,
                    RefreshTokenExpirationTimeInSeconds = 60 * 30,
                    TokenSignedResponseAlg = "RS256",
                    IdTokenSignedResponseAlg = "RS256",
                    AllowedScopes = new List<OAuthScope>
                    {
                        SIDOpenIdConstants.StandardScopes.Email,
                        SIDOpenIdConstants.StandardScopes.OpenIdScope,
                        SIDOpenIdConstants.StandardScopes.Phone,
                        SIDOpenIdConstants.StandardScopes.Profile,
                        SIDOpenIdConstants.StandardScopes.Role,
                        AccountsScope
                    },
                    PreferredTokenProfile = "Bearer",
                    TokenEndPointAuthMethod = "tls_client_auth",
                    ResponseTypes = new List<string>
                    {
                        "id_token", "token", "code"
                    },
                    GrantTypes = new List<string>
                    {
                        "authorization_code", "implicit", "client_credentials", "password"
                    },
                    JsonWebKeys = new List<JsonWebKey>
                    {
                        firstMtlsClientJsonWebKey
                    },
                    TlsClientAuthSubjectDN = "firstMtlsClient"
                },
                new OpenIdClient
                {
                    ClientId = "secondMtlsClient",
                    ClientSecret = "mtsClientSecret",
                    ApplicationKind = ApplicationKinds.Web,
                    ApplicationType = "web",
                    UpdateDateTime = DateTime.UtcNow,
                    CreateDateTime = DateTime.UtcNow,
                    TokenExpirationTimeInSeconds = 60 * 30,
                    RefreshTokenExpirationTimeInSeconds = 60 * 30,
                    TokenSignedResponseAlg = "RS256",
                    IdTokenSignedResponseAlg = "RS256",
                    AllowedScopes = new List<OAuthScope>
                    {
                        SIDOpenIdConstants.StandardScopes.Email,
                        SIDOpenIdConstants.StandardScopes.OpenIdScope,
                        SIDOpenIdConstants.StandardScopes.Phone,
                        SIDOpenIdConstants.StandardScopes.Profile,
                        SIDOpenIdConstants.StandardScopes.Role,
                        AccountsScope
                    },
                    PreferredTokenProfile = "Bearer",
                    TokenEndPointAuthMethod = "tls_client_auth",
                    ResponseTypes = new List<string>
                    {
                        "id_token", "token", "code"
                    },
                    GrantTypes = new List<string>
                    {
                        "authorization_code", "implicit", "client_credentials"
                    },
                    JsonWebKeys = new List<JsonWebKey>
                    {
                        secondMtlsClientJsonWebKey
                    },
                    TlsClientAuthSubjectDN = "secondMtlsClient"
                },
                new OpenIdClient
                {
                    ClientId = "scimClient",
                    ClientSecret = "scimClientSecret",
                    ApplicationKind = ApplicationKinds.Service,
                    TokenEndPointAuthMethod = "client_secret_post",
                    ApplicationType = "web",
                    UpdateDateTime = DateTime.UtcNow,
                    CreateDateTime = DateTime.UtcNow,
                    TokenExpirationTimeInSeconds = 60 * 30,
                    RefreshTokenExpirationTimeInSeconds = 60 * 30,
                    TokenSignedResponseAlg = "RS256",
                    IdTokenSignedResponseAlg = "RS256",
                    AllowedScopes = new List<OAuthScope>
                    {
                        SIDOpenIdConstants.StandardScopes.ScimScope
                    },
                    GrantTypes = new List<string>
                    {
                        "implicit",
                    },
                    RedirectionUrls = new List<string>
                    {
                        "http://localhost:8080",
                        "http://localhost:1700"
                    },
                    PreferredTokenProfile = "Bearer",
                    ResponseTypes = new List<string>
                    {
                        "token",
                        "id_token"
                    }
                },
                new OpenIdClient
                {
                    ClientId = "umaClient",
                    ClientSecret = "umaClientSecret",
                    ApplicationKind = ApplicationKinds.Service,
                    TokenEndPointAuthMethod = "client_secret_post",
                    ApplicationType = "web",
                    UpdateDateTime = DateTime.UtcNow,
                    CreateDateTime = DateTime.UtcNow,
                    TokenExpirationTimeInSeconds = 60 * 30,
                    RefreshTokenExpirationTimeInSeconds = 60 * 30,
                    TokenSignedResponseAlg = "RS256",
                    IdTokenSignedResponseAlg = "RS256",
                    AllowedScopes = new List<OAuthScope>
                    {
                        SIDOpenIdConstants.StandardScopes.OpenIdScope,
                        SIDOpenIdConstants.StandardScopes.Profile,
                        SIDOpenIdConstants.StandardScopes.Email
                    },
                    GrantTypes = new List<string>
                    {
                        "implicit",
                        "authorization_code"
                    },
                    RedirectionUrls = new List<string>
                    {
                        "https://localhost:60001/signin-oidc"
                    },
                    PreferredTokenProfile = "Bearer",
                    ResponseTypes = new List<string>
                    {
                        "token",
                        "id_token",
                        "code"
                    }
                },
                new OpenIdClient
                {
                    ClientId = "simpleIdServerWebsite",
                    ClientSecret = "simpleIdServerWebsiteSecret",
                    ApplicationKind = ApplicationKinds.SPA,
                    TokenEndPointAuthMethod = "pkce",
                    ApplicationType = "web",
                    UpdateDateTime = DateTime.UtcNow,
                    CreateDateTime = DateTime.UtcNow,
                    TokenExpirationTimeInSeconds = 60 * 30,
                    RefreshTokenExpirationTimeInSeconds = 60 * 30,
                    TokenSignedResponseAlg = "RS256",
                    IdTokenSignedResponseAlg = "RS256",
                    AllowedScopes = new List<OAuthScope>
                    {
                        SIDOpenIdConstants.StandardScopes.OpenIdScope,
                        SIDOpenIdConstants.StandardScopes.Profile,
                        SIDOpenIdConstants.StandardScopes.Email,
                        SIDOpenIdConstants.StandardScopes.Role
                    },
                    GrantTypes = new List<string>
                    {
                        "authorization_code",
                        "implicit"
                    },
                    RedirectionUrls = new List<string>
                    {
                        "http://localhost:4200",
                        "https://simpleidserver.northeurope.cloudapp.azure.com/simpleidserver/"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:4200",
                        "https://simpleidserver.northeurope.cloudapp.azure.com/simpleidserver/"
                    },
                    PreferredTokenProfile = "Bearer",
                    ResponseTypes = new List<string>
                    {
                        "token",
                        "id_token",
                        "code"
                    },
                    JsonWebKeys = new List<JsonWebKey>
                    {
                        jsonWebKey
                    }
                },
                new OpenIdClient
                {
                    ClientId = "tradWebsite",
                    ClientSecret = "tradWebsiteSecret",
                    ApplicationKind = ApplicationKinds.Web,
                    TokenEndPointAuthMethod = "client_secret_post",
                    ApplicationType = "web",
                    UpdateDateTime = DateTime.UtcNow,
                    CreateDateTime = DateTime.UtcNow,
                    TokenExpirationTimeInSeconds = 60 * 30,
                    RefreshTokenExpirationTimeInSeconds = 60 * 30,
                    TokenSignedResponseAlg = "RS256",
                    IdTokenSignedResponseAlg = "RS256",
                    AllowedScopes = new List<OAuthScope>
                    {
                        SIDOpenIdConstants.StandardScopes.OpenIdScope,
                        SIDOpenIdConstants.StandardScopes.Profile
                    },
                    GrantTypes = new List<string>
                    {
                        "authorization_code",
                        "password"
                    },
                    RedirectionUrls = new List<string>
                    {
                        "https://localhost:5001/signin-oidc"
                    },
                    PreferredTokenProfile = "Bearer",
                    ResponseTypes = new List<string>
                    {
                        "code",
                        "token",
                        "id_token"
                    }
                },
                new OpenIdClient
                {
                    ClientId = "native",
                    ClientSecret = "nativeSecret",
                    ApplicationKind = ApplicationKinds.Native,
                    TokenEndPointAuthMethod = "pkce",
                    ApplicationType = "web",
                    UpdateDateTime = DateTime.UtcNow,
                    CreateDateTime = DateTime.UtcNow,
                    TokenExpirationTimeInSeconds = 60 * 30,
                    RefreshTokenExpirationTimeInSeconds = 60 * 30,
                    TokenSignedResponseAlg = "RS256",
                    IdTokenSignedResponseAlg = "RS256",
                    AllowedScopes = new List<OAuthScope>
                    {
                        SIDOpenIdConstants.StandardScopes.OpenIdScope,
                        SIDOpenIdConstants.StandardScopes.Profile,
                        SIDOpenIdConstants.StandardScopes.Email
                    },
                    GrantTypes = new List<string>
                    {
                        "authorization_code"
                    },
                    RedirectionUrls = new List<string>
                    {
                        "com.companyname.simpleidserver.mobileapp:/oauth2redirect"
                    },
                    PreferredTokenProfile = "Bearer",
                    ResponseTypes = new List<string>
                    {
                        "code"
                    }
                },
                new OpenIdClient
                {
                    ClientId = "caseManagementWebsite",
                    ClientSecret = "b98113b5-f45f-4a4a-9db5-610b7183e148",
                    ApplicationKind = ApplicationKinds.SPA,
                    TokenEndPointAuthMethod = "pkce",
                    ApplicationType = "web",
                    UpdateDateTime = DateTime.UtcNow,
                    CreateDateTime = DateTime.UtcNow,
                    TokenExpirationTimeInSeconds = 60 * 30,
                    RefreshTokenExpirationTimeInSeconds = 60 * 30,
                    TokenSignedResponseAlg = "RS256",
                    IdTokenSignedResponseAlg = "RS256",
                    AllowedScopes = new List<OAuthScope>
                    {
                        SIDOpenIdConstants.StandardScopes.Profile,
                        SIDOpenIdConstants.StandardScopes.Email,
                        SIDOpenIdConstants.StandardScopes.Role
                    },
                    GrantTypes = new List<string>
                    {
                        "implicit",
                        "authorization_code"
                    },
                    RedirectionUrls = new List<string>
                    {
                        "http://localhost:51724",
                        "http://localhost:8080",
                        "https://simpleidserver.northeurope.cloudapp.azure.com/casemanagement"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:51724",
                        "http://localhost:8080",
                        "https://simpleidserver.northeurope.cloudapp.azure.com/casemanagement"
                    },
                    PreferredTokenProfile = "Bearer",
                    ResponseTypes = new List<string>
                    {
                        "token",
                        "id_token",
                        "code"
                    }
                },
                new OpenIdClient
                {
                    ClientId = "caseManagementTasklistWebsite",
                    ClientSecret = "b98113b5-f45f-4a4a-9db5-610b7183e148",
                    ApplicationKind = ApplicationKinds.SPA,
                    TokenEndPointAuthMethod = "pkce",
                    ApplicationType = "web",
                    UpdateDateTime = DateTime.UtcNow,
                    CreateDateTime = DateTime.UtcNow,
                    TokenExpirationTimeInSeconds = 60 * 30,
                    RefreshTokenExpirationTimeInSeconds = 60 * 30,
                    TokenSignedResponseAlg = "RS256",
                    IdTokenSignedResponseAlg = "RS256",
                    AllowedScopes = new List<OAuthScope>
                    {
                        SIDOpenIdConstants.StandardScopes.Profile,
                        SIDOpenIdConstants.StandardScopes.Email,
                        SIDOpenIdConstants.StandardScopes.Role
                    },
                    GrantTypes = new List<string>
                    {
                        "implicit",
                        "authorization_code"
                    },
                    RedirectionUrls = new List<string>
                    {
                        "http://localhost:51724",
                        "http://localhost:8081",
                        "https://simpleidserver.northeurope.cloudapp.azure.com/tasklist"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:51724",
                        "http://localhost:8081",
                        "https://simpleidserver.northeurope.cloudapp.azure.com/tasklist"
                    },
                    PreferredTokenProfile = "Bearer",
                    ResponseTypes = new List<string>
                    {
                        "token",
                        "id_token",
                        "code"
                    }
                },
                new OpenIdClient
                {
                    ClientId = "caseManagementPerformanceWebsite",
                    ClientSecret = "91894b86-c57e-489a-838d-fb82621a67ee",
                    ApplicationKind = ApplicationKinds.SPA,
                    TokenEndPointAuthMethod = "pkce",
                    ApplicationType = "web",
                    UpdateDateTime = DateTime.UtcNow,
                    CreateDateTime = DateTime.UtcNow,
                    TokenExpirationTimeInSeconds = 60 * 30,
                    RefreshTokenExpirationTimeInSeconds = 60 * 30,
                    TokenSignedResponseAlg = "RS256",
                    IdTokenSignedResponseAlg = "RS256",
                    AllowedScopes = new List<OAuthScope>
                    {
                        SIDOpenIdConstants.StandardScopes.Profile,
                        SIDOpenIdConstants.StandardScopes.Email,
                        SIDOpenIdConstants.StandardScopes.Role
                    },
                    GrantTypes = new List<string>
                    {
                        "implicit",
                        "authorization_code"
                    },
                    RedirectionUrls = new List<string>
                    {
                        "http://localhost:51725",
                        "http://localhost:8081",
                        "https://simpleidserver.northeurope.cloudapp.azure.com/casemanagementperformance"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:51725",
                        "http://localhost:8081",
                        "https://simpleidserver.northeurope.cloudapp.azure.com/casemanagementperformance"
                    },
                    PreferredTokenProfile = "Bearer",
                    ResponseTypes = new List<string>
                    {
                        "token",
                        "id_token",
                        "code"
                    }
                },
                new OpenIdClient
                {
                    ClientId = "medikitWebsite",
                    ClientSecret = "f200eeb0-a6a3-465e-be91-97806e5dd3bc",
                    ApplicationKind = ApplicationKinds.SPA,
                    TokenEndPointAuthMethod = "pkce",
                    ApplicationType = "web",
                    UpdateDateTime = DateTime.UtcNow,
                    CreateDateTime = DateTime.UtcNow,
                    TokenExpirationTimeInSeconds = 60 * 30,
                    RefreshTokenExpirationTimeInSeconds = 60 * 30,
                    TokenSignedResponseAlg = "RS256",
                    IdTokenSignedResponseAlg = "RS256",
                    AllowedScopes = new List<OAuthScope>
                    {
                        SIDOpenIdConstants.StandardScopes.Profile,
                        SIDOpenIdConstants.StandardScopes.Email,
                        SIDOpenIdConstants.StandardScopes.Role
                    },
                    GrantTypes = new List<string>
                    {
                        "implicit",
                        "authorization_code"
                    },
                    RedirectionUrls = new List<string>
                    {
                        "http://localhost:8080",
                        "https://simpleidserver.northeurope.cloudapp.azure.com/medikit"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:8080",
                        "https://simpleidserver.northeurope.cloudapp.azure.com/medikit"
                    },
                    PreferredTokenProfile = "Bearer",
                    ResponseTypes = new List<string>
                    {
                        "token",
                        "id_token",
                        "code"
                    }
                },
                new OpenIdClient
                {
                    ClientId = "gatewayClient",
                    ClientSecret = "gatewayClientPassword",
                    ApplicationKind = ApplicationKinds.Service,
                    Translations = new List<OAuthClientTranslation>
                    {
                        new OAuthClientTranslation
                        {
                            Translation = new OAuthTranslation("gatewayClient_client_name", "SCIMClient", "fr")
                            {
                                Type = "client_name"
                            }
                        }
                    },
                    TokenEndPointAuthMethod = "client_secret_post",
                    UpdateDateTime = DateTime.UtcNow,
                    CreateDateTime = DateTime.UtcNow,
                    TokenExpirationTimeInSeconds = 60 * 30,
                    RefreshTokenExpirationTimeInSeconds = 60 * 30,
                    TokenSignedResponseAlg = "RS256",
                    AllowedScopes = new List<OAuthScope>
                    {
                        ManageClientsScope,
                        ManageScopesScope,
                        ManageUsers,
                        QueryScimResource,
                        AddScimResource,
                        BulkScimResource,
                        UpdateScimResource,
                        DeleteScimResource,
                        ScimProvision,
                        ManageAuthSchemeProviders,
                        ManageRelyingParties
                    },
                    GrantTypes = new List<string>
                    {
                        "client_credentials"
                    },
                    PreferredTokenProfile = "Bearer"
                },
                new OpenIdClient
                {
                    ClientId = "provisioningClient",
                    ClientSecret = "provisioningClientSecret",
                    ApplicationKind = ApplicationKinds.Service,
                    Translations = new List<OAuthClientTranslation>
                    {
                        new OAuthClientTranslation
                        {
                            Translation = new OAuthTranslation("provisioningClient_client_name", "ProvisioningClient", "fr")
                            {
                                Type = "client_name"
                            }
                        }
                    },
                    TokenEndPointAuthMethod = "client_secret_post",
                    UpdateDateTime = DateTime.UtcNow,
                    CreateDateTime = DateTime.UtcNow,
                    TokenExpirationTimeInSeconds = 60 * 30,
                    RefreshTokenExpirationTimeInSeconds = 60 * 30,
                    TokenSignedResponseAlg = "RS256",
                    AllowedScopes = new List<OAuthScope>
                    {
                        ManageUsers,
                        AddScimResource,
                        DeleteScimResource,
                        UpdateScimResource,
                        BulkScimResource,
                        QueryScimResource,
                        ManageAuthSchemeProviders
                    },
                    GrantTypes = new List<string>
                    {
                        "client_credentials"
                    },
                    PreferredTokenProfile = "Bearer"
                },
                new OpenIdClient
                {
                    ClientId = "bpmnClient",
                    ClientSecret = "bpmnClientSecret",
                    ApplicationKind = ApplicationKinds.Service,
                    Translations = new List<OAuthClientTranslation>
                    {
                        new OAuthClientTranslation
                        {
                            Translation = new OAuthTranslation("bpmnClient_client_name", "BpmnClient", "fr")
                            {
                                Type = "client_name"
                            }
                        }
                    },
                    TokenEndPointAuthMethod = "client_secret_post",
                    UpdateDateTime = DateTime.UtcNow,
                    CreateDateTime = DateTime.UtcNow,
                    TokenExpirationTimeInSeconds = 60 * 30,
                    RefreshTokenExpirationTimeInSeconds = 60 * 30,
                    TokenSignedResponseAlg = "RS256",
                    AllowedScopes = new List<OAuthScope>
                    {
                        ManageHumanTaskInstance
                    },
                    GrantTypes = new List<string>
                    {
                        "client_credentials"
                    },
                    PreferredTokenProfile = "Bearer"
                },
                new OpenIdClient
                {
                    ClientId = "humanTaskClient",
                    ClientSecret = "humanTaskClientSecret",
                    ApplicationKind = ApplicationKinds.Service,
                    Translations = new List<OAuthClientTranslation>
                    {
                        new OAuthClientTranslation
                        {
                            Translation = new OAuthTranslation("humanTaskClient_client_name", "HumanTaskClient", "fr")
                            {
                                Type = "client_name"
                            }
                        }
                    },
                    TokenEndPointAuthMethod = "client_secret_post",
                    UpdateDateTime = DateTime.UtcNow,
                    CreateDateTime = DateTime.UtcNow,
                    TokenExpirationTimeInSeconds = 60 * 30,
                    RefreshTokenExpirationTimeInSeconds = 60 * 30,
                    TokenSignedResponseAlg = "RS256",
                    AllowedScopes = new List<OAuthScope>
                    {
                        ManageUsers,
                        ManageHumanTaskInstance
                    },
                    GrantTypes = new List<string>
                    {
                        "client_credentials"
                    },
                    PreferredTokenProfile = "Bearer"
                }
            };
        }
    }
}