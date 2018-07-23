using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using SocialNetwork.Configuration;
using SocialNetwork.Security.Models;

namespace SocialNetwork.Security
{
    public class TokenManager : ITokenManager
    {
        public TokenDefinition CreateTokenDefinition(IEnumerable<Claim> claims)
        {
            var now = DateTime.Now;

            var accessToken = CreateAccessToken(claims, now);
            var refreshToken = CreateRefreshToken(claims, now);
            
            return new TokenDefinition
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                CreatedAt = now,
                AccessTokenExpiresAt = now.Add(JwtTokenDefinitions.TokenExpirationTime),
                RefreshTokenExpiresAt = now.Add(JwtTokenDefinitions.RefreshTokenExpirationTime)
            };
        }
        
        private string CreateAccessToken(IEnumerable<Claim> claims, DateTime now)
        {
            var jwt = new JwtSecurityToken(
                issuer: JwtTokenDefinitions.Issuer,
                audience: JwtTokenDefinitions.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(JwtTokenDefinitions.TokenExpirationTime),
                signingCredentials: JwtTokenDefinitions.SigningCredentials);
            
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        private string CreateRefreshToken(IEnumerable<Claim> claims, DateTime now)
        {
            var handler = new JwtSecurityTokenHandler();
            var refreshToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = JwtTokenDefinitions.Issuer,
                Audience = JwtTokenDefinitions.Audience,
                SigningCredentials = JwtTokenDefinitions.SigningCredentials,
                Subject =  new ClaimsIdentity(claims.Where(c => c.Type == ClaimType.UserId)),
                Expires = now.Add(JwtTokenDefinitions.RefreshTokenExpirationTime),
                NotBefore = DateTime.Now
            });

            return handler.WriteToken(refreshToken);
        }
    }
}