﻿// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Newtonsoft.Json.Linq;
using SimpleIdServer.Jwt.Jws;
using SimpleIdServer.Jwt.Jws.Handlers;
using System.Linq;
using System.Net.Http;
using TechTalk.SpecFlow;
using Xunit;

namespace SimpleIdServer.OAuth.Host.Acceptance.Tests.Steps
{
    [Binding]
    public class ValidationSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public ValidationSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }


        [Then("JSON exists '(.*)'")]
        public void ThenExists(string key)
        {
            var jsonHttpBody = _scenarioContext["jsonHttpBody"] as JObject;
            Assert.True(jsonHttpBody.ContainsKey(key));
        }

        [Then("JSON '(.*)'='(.*)'")]
        public void ThenEqualsTo(string key, string value)
        {
            var jsonHttpBody = _scenarioContext["jsonHttpBody"] as JObject;
            Assert.Equal(value, jsonHttpBody[key].ToString());
        }

        [Then("HTTP status code equals to '(.*)'")]
        public void ThenCheckHttpStatusCode(int code)
        {
            var httpResponseMessage = _scenarioContext["httpResponseMessage"] as HttpResponseMessage;
            Assert.Equal(code, (int)httpResponseMessage.StatusCode);
        }

        [Then("Extract JWS payload from '(.*)' and check claim '(.*)' is array")]
        public void ThenJWSPayloadClaimContains(string key, string name)
        {
            var jsonHttpBody = _scenarioContext["jsonHttpBody"] as JObject;
            var json = jsonHttpBody[key].ToString();
            var jsonPayload = new JwsGenerator(new ISignHandler[0]).ExtractPayload(json);
            var jArr = JArray.Parse(jsonPayload[name].ToString());
            Assert.NotNull(jArr);
        }

        [Then("Extract JWS payload from '(.*)' and check claim '(.*)' contains '(.*)'")]
        public void ThenJWSPayloadClaimEqualsTo(string key, string name, string value)
        {
            var jsonHttpBody = _scenarioContext["jsonHttpBody"] as JObject;
            var json = jsonHttpBody[key].ToString();
            var jsonPayload = new JwsGenerator(new ISignHandler[0]).ExtractPayload(json);
            var jArr = JArray.Parse(jsonPayload[name].ToString()).Select(s => s.ToString());
            Assert.True(jArr.Contains(value));
        }
    }
}
