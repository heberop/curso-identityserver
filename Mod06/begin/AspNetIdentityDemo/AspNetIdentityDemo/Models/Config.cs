using System;
using System.Collections.Generic;
using IdentityServer4.Models;
using System.Security.Claims;

namespace AspNetIdentityDemo.Models
{
    public static class Config
    {
        public static IEnumerable<Client> Clients() =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "mvc-client",
                    ClientName = "MVC Client",
                    ClientSecrets = { new Secret("segredo".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Implicit,
                    //AllowedGrantTypes = GrantTypes.Code,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = {
                        "http://localhost:9090/signin-oidc",
                        "http://localhost:55158/signin-oidc"
                    },

                    AllowedScopes = { "openid", "profile", "email" },

                }
            };

        public static IEnumerable<IdentityResource> IdentityResources() =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };

        public static IEnumerable<ApiResource> ApiResources() =>
            new List<ApiResource>
            {
                new ApiResource("financial")
                {
                    Scopes = new List<Scope>
                    {
                        new Scope("financial.read"),
                        new Scope("financial.write"),
                    }
                }
            };
    }
}
