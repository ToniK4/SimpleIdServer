using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluePoles.IdentityProvider.Tests.Infrastructure
{

    public class TestSettings : IAppSettings
    {
        public string Setting1 { get; set; } = default!;

    }
}
