using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace MvcClientCode
{
    public class CustomClaimsTransformer : IClaimsTransformer
    {
        public async Task<ClaimsPrincipal> TransformAsync(ClaimsTransformationContext context)
        {
            var identity = (ClaimsIdentity)context.Principal.Identity;

            //buscar novas claims e adicionar ao principal
            identity.AddClaim(new Claim(ClaimTypes.Country, "Brasil"));
            identity.AddClaim(new Claim("financial", "read write"));

            return await Task.FromResult(context.Principal);
        }
    }
}
