using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace SocialNetwork.Security.Handlers
{
    public interface IAuthorizationHandler
    {
        Task HandleAsync(AuthorizationContext context);
    }
}