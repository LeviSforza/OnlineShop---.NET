using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lista10
{
    public class NotAdminRequirement : IAuthorizationRequirement
    {
        public NotAdminRequirement() { }
    }

    public class NotAdminHandler : AuthorizationHandler<NotAdminRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, NotAdminRequirement requirement)
        {
            var user = context.User;
            if (user.IsInRole("Admin"))
                return Task.CompletedTask;
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }

}
