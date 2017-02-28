using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;

namespace HostDemo.Stores
{
    public class ResourceStore : IResourceStore
    {
        public async Task<ApiResource> FindApiResourceAsync(string name)
        {
            return Config.ApiResources().FirstOrDefault(r => r.Name == name);
        }

        public async Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            return Config.ApiResources()
                         .Where(r => scopeNames.Contains(r.Name)
                             || r.Scopes.Any(s => scopeNames.Contains(s.Name)));
        }

        public async Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            return Config.IdentityResources()
                         .Where(r => scopeNames.Contains(r.Name));
        }

        public async Task<Resources> GetAllResources()
        {
            return new Resources(Config.IdentityResources(), Config.ApiResources());
        }
    }
}
