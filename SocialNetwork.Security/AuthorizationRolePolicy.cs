using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace SocialNetwork.Security.Provider
{
    public class AuthorizationRolePolicy
    {
        public AuthorizationRolePolicy(IEnumerable<string> roles)
        {
            Roles = roles;
        }

        public IEnumerable<string> Roles { get; private set; }
        
        public static async Task<AuthorizationRolePolicy> CombineAsync(IEnumerable<IAuthorizeData> authorizeData)
        {
            if (authorizeData == null)
            {
                throw new ArgumentNullException(nameof(authorizeData));
            }

            IEnumerable<string> roles = Enumerable.Empty<string>();
            foreach (var authorizeDatum in authorizeData)
            {
                var rolesSplit = authorizeDatum.Roles?.Split(',');
                if (rolesSplit != null && rolesSplit.Any())
                {
                    var trimmedRolesSplit = rolesSplit.Where(r => !string.IsNullOrWhiteSpace(r)).Select(r => r.Trim());
                    roles = trimmedRolesSplit;
                }
              
            }
            return new AuthorizationRolePolicy(roles);
        }
    }
}