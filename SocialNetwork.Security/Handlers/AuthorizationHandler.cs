using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using SocialNetwork.Security.Models;

namespace SocialNetwork.Security.Handlers
{
    public class RolesAuthorizationRequirement  : AuthorizationHandler<RolesAuthorizationRequirement>, IAuthorizationRequirement
    {
        public IEnumerable<string> AllowedRoles { get; }
        
        public RolesAuthorizationRequirement(IEnumerable<string> allowedRoles)
        {
            if (allowedRoles == null)
            {
                throw new ArgumentNullException(nameof(allowedRoles));
            }

            if (!allowedRoles.Any())
            {
                throw new InvalidOperationException();
            }
            
            AllowedRoles = allowedRoles;
        }
        
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RolesAuthorizationRequirement requirement)
        {
            if (context.User != null)
            {
                if (requirement.AllowedRoles == null || !requirement.AllowedRoles.Any())
                {
                    return Task.CompletedTask;
                }

                var roles = context.User.Claims
                    .Where(c => c.Type == ClaimType.Role)
                    .Select(c => c.Value);

                var found = requirement.AllowedRoles.Any(r => roles.Contains(r, StringComparer.OrdinalIgnoreCase));
                
                if (found)
                {
                    context.Succeed(requirement);
                }
            }
            
            return Task.CompletedTask;
        }
    }
}