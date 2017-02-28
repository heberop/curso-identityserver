using System.Collections.Generic;
using IdentityServer4.Models;

namespace IdentityServerDemo.Config
{
    public class ApiResources
    {
        public static IEnumerable<ApiResource> InMemoryApiResources => new List<ApiResource>
        {
            new ApiResource(Constants.Resources.HumanResourcesApi, "Human Resource API")
            {
                Scopes =
                {
                    new Scope(Constants.Resources.HumanResourcesFullAccess, "Acesso completo"),
                    new Scope(Constants.Resources.HumanResourcesReadOnly, "Acesso somente leitura")
                }
            }
        };
    }
}