using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.Security.Provider;

namespace SocialNetwork.Security.Attribute
{
    public class AuthorizationAttribute : System.Attribute,  IAuthorizeData , IFilterFactory
    {
        public string Policy { get; set; }
        public string Roles { get; set; }
        public string AuthenticationSchemes { get; set; }
        
        
        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
           return AuthorizationApplicationModelProvider.GetFilter( new [] { this });
        }

        public bool IsReusable => true;
    }
    
    public class AuthorizationFilter : IAsyncAuthorizationFilter
    {
        public AuthorizationFilter(AuthorizationRolePolicy policy)
        {
            Policy = policy;
        }

        public AuthorizationRolePolicy Policy { get; }
        
        public bool IsReusable => true;
        
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var authorizationService = context.HttpContext.RequestServices.GetRequiredService<IAuthorizationService>();
            var result = await authorizationService.AuthorizeAsync(context.HttpContext.User, Policy);

            if (!result.Succeeded)
            {
                context.Result = new ForbidResult("Jwt");
            }
        }
    }
}