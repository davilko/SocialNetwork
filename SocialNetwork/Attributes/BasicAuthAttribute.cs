using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using SocialNetwork.Business.Contract;
using SocialNetwork.Repository.Contract;
using SocialNetwork.Repository.Contract.Models;
using SocialNetwork.Repository.Spicification;
using SocialNetwork.Utility.Cryptography;

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
            string authHeader = context.HttpContext.Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(authHeader))
            {
                context.Result = new BadRequestObjectResult("No Authorization header");
                return;
            }

            if (!authHeader.StartsWith(AUTH_SCHEMA, StringComparison.OrdinalIgnoreCase))
            {
                context.Result = new BadRequestObjectResult("No Basic Authorization");
                return;
            }

            var encodedCredentials = authHeader.Substring(AUTH_SCHEMA.Length).Trim();

            if (string.IsNullOrEmpty(encodedCredentials))
            {
                const string noCredentialsMessage = "No credentials";
                context.Result = new BadRequestObjectResult(noCredentialsMessage);
                return;
            }

            var decodedCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials));

            var delimiterIndex = decodedCredentials.IndexOf(':');
            if (delimiterIndex == -1)
            {
                const string missingDelimiterMessage = "Invalid credentials, missing delimiter.";
                context.Result = new BadRequestObjectResult(missingDelimiterMessage);
                return;
            }

            var username = decodedCredentials.Substring(0, delimiterIndex);
            var password = decodedCredentials.Substring(delimiterIndex + 1);

            var identityManager = context.HttpContext
                .RequestServices
                .GetService(typeof(IIdentityManager)) as IIdentityManager;

            var identityUser = identityManager.GetUserIdentity(username, password)
                .ConfigureAwait(false).GetAwaiter().GetResult();

            if (identityUser == null)
            {
                throw new Exception();
            }

            var claims = new List<Claim>(identityUser.Roles.Count + 1);
            claims.Add(new Claim("uuid", identityUser.UserId.ToString()));
            foreach (var userRole in identityUser.Roles)
            {
                claims.Add(new Claim("role", userRole));
            }

            context.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims, "SocialNetwork"));
        }
    }
}