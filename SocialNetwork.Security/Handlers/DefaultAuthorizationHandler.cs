using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace SocialNetwork.Security.Handlers
{
    public abstract class DefaultAuthorizationHandler<TRequirement> : IAuthorizationHandler
    {
        public async Task HandleAsync(AuthorizationContext context)
        {
            if (context.Requirement is TRequirement authorizationRequirement)
            {
                await HandleRequirementAsync(context, authorizationRequirement);
            }
        }

        protected abstract Task HandleRequirementAsync(AuthorizationContext context, TRequirement requirement);
    }
}