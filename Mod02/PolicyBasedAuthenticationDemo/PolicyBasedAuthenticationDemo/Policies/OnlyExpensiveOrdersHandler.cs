using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolicyBasedAuthenticationDemo.Policies
{
    public class OnlyExpensiveOrdersHandler : AuthorizationHandler<OnlyExpensiveOrdersRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            OnlyExpensiveOrdersRequirement requirement)
        {
            if(context.User.HasClaim(c => c.Type == "manager"))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
