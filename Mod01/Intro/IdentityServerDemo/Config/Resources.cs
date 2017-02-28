using System.Collections.Generic;
using IdentityServer4.Models;

namespace IdentityServerDemo.Config
{
    public static class Resources
    {
        public static IEnumerable<IdentityResource> InMemoryIdentityResources => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email()
        };
    }
}