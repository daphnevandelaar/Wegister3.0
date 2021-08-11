// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityServer4;

namespace IdentityProvider
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),

                // let's include the role claim in the profile
                new ProfileWithRoleIdentityResource(),
                new IdentityResources.Email()
            };


        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                // the api requires the role claim
                new ApiResource("wegister", "Wegister", new[] { JwtClaimTypes.Role })
            };


        public static IEnumerable<Client> Clients =>
            new []
            {
                new Client
                {
                    ClientId = "wegisterUI",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedCorsOrigins = { "https://localhost:44346" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "wegister",
                        "email"
                    },
                    RedirectUris = { "https://localhost:44346/authentication/login-callback" },
                    PostLogoutRedirectUris = { "https://localhost:44346/authentication/logout-callback" }
                },
            };
    }
}