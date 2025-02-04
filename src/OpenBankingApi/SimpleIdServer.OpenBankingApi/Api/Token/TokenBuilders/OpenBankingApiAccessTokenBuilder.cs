﻿// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using SimpleIdServer.Jwt.Jws;
using SimpleIdServer.OAuth.Api;
using SimpleIdServer.OAuth.Helpers;
using SimpleIdServer.OAuth.Jwt;
using SimpleIdServer.OAuth.Options;
using SimpleIdServer.OAuth.Persistence;
using SimpleIdServer.OpenID.Api.Token.TokenBuilders;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleIdServer.OpenBankingApi.Api.Token.TokenBuilders
{
    public class OpenBankingApiAccessTokenBuilder: OpenIDAccessTokenBuilder
    {
        private readonly IOpenBankingApiAuthRequestEnricher _openBankingApiAuthRequestEnricher;

        public OpenBankingApiAccessTokenBuilder(
            IGrantedTokenHelper grantedTokenHelper,
            IJwtBuilder jwtBuilder,
            IOptions<OAuthHostOptions> options,
            IOpenBankingApiAuthRequestEnricher openBankingApiAuthRequestEnricher, IOAuthClientRepository oauthClientRepository) : base(grantedTokenHelper, jwtBuilder, oauthClientRepository)
        {
            _openBankingApiAuthRequestEnricher = openBankingApiAuthRequestEnricher;
        }

        protected override async Task<JwsPayload> BuildOpenIdPayload(IEnumerable<string> scopes, JObject queryParameters, HandlerContext handlerContext, CancellationToken cancellationToken)
        {
            var result = await base.BuildOpenIdPayload(scopes, queryParameters, handlerContext, cancellationToken);
            await _openBankingApiAuthRequestEnricher.Enrich(result, queryParameters, cancellationToken);
            return result;
        }
    }
}
