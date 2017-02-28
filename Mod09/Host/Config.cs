using System;
using System.Collections.Generic;
using IdentityServer4.Models;
using System.Security.Claims;

namespace WebApplication4
{
    public static class Config
    {
        public static IEnumerable<Client> Clients() =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "mvc-client-implicit",
                    ClientName = "MVC Client Implicit",
                    ClientSecrets = { new Secret("segredo".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = {
                        "http://localhost:9010/signin-oidc",
                    },
                    LogoutUri = "http://localhost:9010/signout-oidc",
                    PostLogoutRedirectUris =
                    {
                        "http://localhost:9010/signout-callback-oidc"
                    },

                    AllowedScopes = { "openid", "profile", "email", "financial.read" }
                },

                new Client
                {
                    ClientId = "mvc-client-code",
                    ClientName = "MVC Client Code",
                    ClientSecrets = { new Secret("segredo".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,

                    RedirectUris = {
                        "http://localhost:9020/signin-oidc",
                    },
                    LogoutUri = "http://localhost:9020/signout-oidc",
                    PostLogoutRedirectUris =
                    {
                        "http://localhost:9020/signout-callback-oidc"
                    },

                    AllowedScopes = { "openid", "profile", "email", "financial.read" },

                    //Refresh Token Settings
                    AllowOfflineAccess = true,
                    RefreshTokenUsage = TokenUsage.ReUse,
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    SlidingRefreshTokenLifetime = 60 * 60 * 24 * 365
                },

                new Client
                {
                    ClientId = "server-to-server",
                    ClientName = "Server Access",
                    ClientSecrets = { new Secret("segredo".Sha256()) },

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "profile", "financial.read", "financial.write", "marketing" }
                },

                new Client
                {
                    ClientId = "js-client",
                    ClientName = "SPA Client Implicit",
                    ClientSecrets = { new Secret("segredo".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = {
                        "http://localhost:9030/index.html",
                        "http://localhost:9030/callback.html",
                        "http://localhost:9030/silent.html",
                        "http://localhost:9030/popup.html",
                    },
                    PostLogoutRedirectUris =
                    {
                        "http://localhost:9030/index.html"
                    },

                    AllowedCorsOrigins = { "http://localhost:9030" },

                    AllowedScopes = { "openid", "profile", "email", "financial.read" }
                },

                new Client
                {
                    ClientId = "mobile-client",
                    ClientName = "Mobile Client Resource Owner",
                    ClientSecrets = { new Secret("segredo".Sha256()) },

                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    AllowedScopes = { "openid", "profile", "email", "financial.read" }
                },

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
                    ApiSecrets = { new Secret("segredo".Sha256()) },
                    Scopes = new List<Scope>
                    {
                        new Scope("financial.read"),
                        new Scope("financial.write"),
                    }
                },
                new ApiResource("marketing")
                {
                    ApiSecrets = { new Secret("segredo".Sha256()) },
                }
            };
    }
}
