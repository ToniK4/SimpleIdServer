using BluePoles.IdentityProvider.OpenId.Management;
using BluePoles.IdentityProvider.Tests.Infrastructure;
using MassTransit.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluePoles.IdentityProvider.Tests
{
    [TestFixture]
    internal class TestInfastructure : BaseTest
    {
        [Test]
        public async Task TestSetup()
        {
            var settings = Services.GetRequiredService<IAppSettings>();
            Assert.IsTrue(settings is not null && settings.Setting1 == "value");

            var options = Services.GetRequiredService<IOptions<BPOpenIDAuthenticatorOptions>>();
            Assert.IsTrue(options.Value is not null && options.Value.ClientSecret is not null);
            await Task.CompletedTask;
        }
    }
}
