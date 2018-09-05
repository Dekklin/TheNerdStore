using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TheNerdStore.Models.Handlers
{
    public class IsMemberHandler : AuthorizationHandler<IsMemberReq>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsMemberReq requirement)
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.Email))
            {
                return Task.CompletedTask;
            }

            string userEmail = context.User.FindFirst(e => e.Type == ClaimTypes.Email).Value;

            if (userEmail.Contains(requirement.Email))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
