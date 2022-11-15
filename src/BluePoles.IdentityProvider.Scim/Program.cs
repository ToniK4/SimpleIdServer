﻿// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BluePoles.IdentityProvider.Scim
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults((cfg) =>
                {
                    //cfg.UseUrls("https://localhost:60002");
                    cfg.UseStartup<Startup>();
                })
                .Build();
            host.Run();
        }
    }
}