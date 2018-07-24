using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using SocialNetwork.Security.Internal;
using SocialNetwork.Security.Provider;

namespace SocialNetwork.Security
{
    public interface IAuthorizationService
    {
        Task<AuthorizationResult> AuthorizeAsync(ClaimsPrincipal user, AuthorizationRolePolicy policy);
    }
}