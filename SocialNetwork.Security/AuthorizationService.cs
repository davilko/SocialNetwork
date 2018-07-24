using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using SocialNetwork.Security.Handlers;
using SocialNetwork.Security.Provider;
using AuthorizationResult = SocialNetwork.Security.Internal.AuthorizationResult;

namespace SocialNetwork.Security
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IEnumerable<IAuthorizationHandler> _handlers;

        public AuthorizationService(IEnumerable<IAuthorizationHandler> handlers)
        {
            _handlers = handlers;
        }

        public async Task<AuthorizationResult> AuthorizeAsync(ClaimsPrincipal user, AuthorizationRolePolicy policy)
        {
            var context = new AuthorizationContext(policy.RolesRequirement, user);
            foreach (var authorizationHandler in _handlers)
            {
                await authorizationHandler.HandleAsync(context);
            }

            if (context.IsAuthrize)
            {
                return AuthorizationResult.Success;
            }
            
            return AuthorizationResult.Failed;
        }
    }
}