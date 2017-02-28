using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;

namespace HostDemo.Stores
{
    public class ClientStore : IClientStore
    {
        // Como os stores ficam registrados no container de DI
        // é possível injetar dependencias nesta classe
        public ClientStore() { }
        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            return Config.Clients().FirstOrDefault(c => c.ClientId == clientId);
        }
    }
}
