using Microsoft.AspNetCore.Authorization;
using ResourceBasedAuthenticationDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ResourceBasedAuthorizationDemo.Policies
{
    public class OnlyOwnOrdersAuthorizationHandler 
        : AuthorizationHandler<OnlyOwnOrdersRequirement, Order>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            OnlyOwnOrdersRequirement requirement, 
            Order resource)
        {
            var id = Convert.ToInt32(context.User.Claims.FirstOrDefault(
                c => c.Type == ClaimTypes.NameIdentifier));

            if (id == resource.UserId)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
