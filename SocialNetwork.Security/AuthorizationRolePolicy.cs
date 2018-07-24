using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using SocialNetwork.Security.Handlers;

namespace SocialNetwork.Security.Provider
{
    public class AuthorizationRolePolicy
    {
        public AuthorizationRolePolicy(IAuthorizationRequirement roles)
        {
            RolesRequirement = roles;
        }

        public IAuthorizationRequirement RolesRequirement { get; private set; }
        
        public static Task<AuthorizationRolePolicy> CombineAsync(IEnumerable<IAuthorizeData> authorizeData)
        {
            if (authorizeData == null)
            {
                throw new ArgumentNullException(nameof(authorizeData));
            }

            RolesAuthorizationRequirement rolesAuthorizationRequirement = new RolesAuthorizationRequirement(Enumerable.Empty<string>());
            foreach (var authorizeDatum in authorizeData)
            {
                var rolesSplit = authorizeDatum.Roles?.Split(',');
                if (rolesSplit != null && rolesSplit.Any())
                {
                    var trimmedRolesSplit = rolesSplit.Where(r => !string.IsNullOrWhiteSpace(r)).Select(r => r.Trim());
                    rolesAuthorizationRequirement = new RolesAuthorizationRequirement(trimmedRolesSplit);
                }
              
            }
            return Task.FromResult(new AuthorizationRolePolicy(rolesAuthorizationRequirement));
        }
    }
}