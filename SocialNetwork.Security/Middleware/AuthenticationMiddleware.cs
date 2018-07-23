using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SocialNetwork.Configuration;
using SocialNetwork.Security.Configuration;
using SocialNetwork.Security.Models;

namespace SocialNetwork.Security.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _tokenPath;

        private readonly JwtSecurityTokenHandler _handler;
        private readonly TokenValidationParameters _validationParameters;
        private readonly ICookieManager _cookieManager;
        
        public AuthenticationMiddleware(RequestDelegate next, string tokenPath)
        {
            _next = next;
            _tokenPath = tokenPath;
            
            _handler = new JwtSecurityTokenHandler();
            _cookieManager = new CookieManager();
            _validationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = JwtTokenDefinitions.IssuerSigningKey,
                ValidAudience = JwtTokenDefinitions.Audience,
                ValidIssuer = JwtTokenDefinitions.Issuer,
                ValidateIssuerSigningKey = JwtTokenDefinitions.ValidateIssuerSigningKey,
                ValidateLifetime = JwtTokenDefinitions.ValidateLifetime,
                ClockSkew = JwtTokenDefinitions.ClockSkew,
                RequireSignedTokens = true
            };
        }
        
        public async Task InvokeAsync(HttpContext context)
        {
            //Basic auth
            if (context.Request.Path.Value.StartsWith(_tokenPath, StringComparison.OrdinalIgnoreCase))
            {
                await _next(context);
            }

            string authHeader = context.Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(authHeader))
            {
                // TODO error
            }

            var tokenStartIndex = authHeader.IndexOf(" ", StringComparison.Ordinal);

            var token = authHeader.Substring(tokenStartIndex).Trim();

            if (_handler.CanReadToken(token))
            {
                ClaimsPrincipal principal;
                try
                {
                    principal = _handler.ValidateToken(token, _validationParameters, out var validatedToken);
                    context.User = principal;
                }
                catch (SecurityTokenExpiredException ex)
                {
                    
                }
                catch (Exception e)
                {
                    throw;
                }

                await _next(context);

            }
            else
            {
                // TODO error
            }
        }

        private void SetCookie(HttpContext context, TokenDefinition tokenDefinition)
        {
            _cookieManager.SetCookie(
                context, 
                Cookie.AccessToken, 
                tokenDefinition.AccessToken, 
                tokenDefinition.AccessTokenExpiresAt);
            
            _cookieManager.SetCookie(
                context,
                Cookie.RefreshToken,
                tokenDefinition.RefreshToken,
                tokenDefinition.RefreshTokenExpiresAt);
        }
    }
}