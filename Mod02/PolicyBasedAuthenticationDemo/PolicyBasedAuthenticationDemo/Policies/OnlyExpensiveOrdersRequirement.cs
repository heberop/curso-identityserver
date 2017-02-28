using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolicyBasedAuthenticationDemo.Policies
{
    public class OnlyExpensiveOrdersRequirement : IAuthorizationRequirement
    {
        private readonly decimal minValue;
        public OnlyExpensiveOrdersRequirement(decimal minValue)
        {
            this.minValue = minValue;
        }
    }
}
