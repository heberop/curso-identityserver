using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace IdentityServerDemo.Config
{
    public class Clients
    {
        public static IEnumerable<Client> InMemoryClients => new List<Client>
        {
            new Client
            {
                ClientName = "MVC App",
                ClientId = "MVCApp",
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,
                AllowedScopes = new List<string>()
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    Constants.Resources.HumanResourcesApi,
                    Constants.Resources.HumanResourcesFullAccess,
                    Constants.Resources.HumanResourcesReadOnly,
                },
                

                RedirectUris = new[] { "http://localhost:9000/signin-oidc" }
            }
        };
    }
}