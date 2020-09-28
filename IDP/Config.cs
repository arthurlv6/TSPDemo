// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IDP
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource("roleclaim", new [] { "roleclaim" })
            };


        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                new ApiResource("tspapi", 
                    "tsp API", 
                    new [] { "roleclaim" })
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            { 
                new Client
                {
                    ClientId = "tsp",
                    ClientName = "tsp APP",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequirePkce = true,
                    RedirectUris = { "https://tsp-dev-app.azurewebsites.net/authentication/login-callback" },
                    PostLogoutRedirectUris = { "https://tsp-dev-app.azurewebsites.net/authentication/logout-callback" },
                    AllowedScopes = { "openid", "profile", "email", "tspapi", "roleclaim" },
                    AllowedCorsOrigins = { "https://tsp-dev-app.azurewebsites.net" },
                    RequireConsent = true
                }             
            };
    }
}