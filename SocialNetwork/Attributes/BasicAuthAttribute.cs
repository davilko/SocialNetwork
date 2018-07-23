using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SocialNetwork.Business.Contract;
using SocialNetwork.Security.Models;

namespace SocialNetwork.Attributes
{
    public class BasicAuthAttribute : TypeFilterAttribute
    {
        public BasicAuthAttribute() : base(typeof(BasicAuthFilter))
        {
        }
    }

    public class BasicAuthFilter : IAuthorizationFilter
    {
        private const string AUTH_SCHEMA = "Basic";
        
        public void OnAuthorization(AuthorizationFilterContext context)
        {

            var credentials = GetCredentials(context);

            if (credentials == null)
            {
                return;
            }

            if (!(context.HttpContext
                .RequestServices
                .GetService(typeof(IIdentityManager)) is IIdentityManager identityManager))
            {
                throw new Exception();
            }
            
            var identityUser = identityManager
                .GetUserIdentity(credentials.Value.userName, credentials.Value.password)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            if (identityUser == null)
            {
                throw new Exception();
            }

            var claims = new List<Claim>(identityUser.Roles.Count + 1)
            {
                new Claim(ClaimType.UserId, identityUser.UserId.ToString())
            };
            
            foreach (var userRole in identityUser.Roles)
            {
                claims.Add(new Claim(ClaimType.Role, userRole));
            }

            context.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims, AUTH_SCHEMA));
        }

        private (string userName, string password)? GetCredentials(AuthorizationFilterContext context)
        {
            string authHeader = context.HttpContext.Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(authHeader))
            {
                context.Result = new BadRequestObjectResult("No Authorization header");
                return null;
            }

            if (!authHeader.StartsWith(AUTH_SCHEMA, StringComparison.OrdinalIgnoreCase))
            {
                context.Result = new BadRequestObjectResult("No Basic Authorization");
                return null;
            }

            var encodedCredentials = authHeader.Substring(AUTH_SCHEMA.Length).Trim();

            if (string.IsNullOrEmpty(encodedCredentials))
            {
                const string noCredentialsMessage = "No credentials";
                context.Result = new BadRequestObjectResult(noCredentialsMessage);
                return null;
            }

            var decodedCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials));

            var delimiterIndex = decodedCredentials.IndexOf(':');
            if (delimiterIndex == -1)
            {
                const string missingDelimiterMessage = "Invalid credentials, missing delimiter.";
                context.Result = new BadRequestObjectResult(missingDelimiterMessage);
                return null;
            }

            var username = decodedCredentials.Substring(0, delimiterIndex);
            var password = decodedCredentials.Substring(delimiterIndex + 1);

            return (username, password);
        }
    }
}