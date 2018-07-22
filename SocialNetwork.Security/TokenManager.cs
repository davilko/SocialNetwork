using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using SocialNetwork.Business.Contract.Models;
using SocialNetwork.Configuration;

namespace SocialNetwork.Security
{
    public class TokenManager : ITokenManager
    {
        public TokenDefinition CreateTokenDefinition(IEnumerable<Claim> claims)
        {
            var now = DateTime.Now;
            var expires = now.Add(JwtTokenDefinitions.TokenExpirationTime);
            var jwt = new JwtSecurityToken(
                issuer: JwtTokenDefinitions.Issuer,
                audience: JwtTokenDefinitions.Audience,
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: JwtTokenDefinitions.SigningCredentials);
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            var refreshToken = CreateRefreshToken(claims);
            
            return new TokenDefinition
            {
                AccessToken = token,
                RefreshToken = refreshToken,
                CreatedAt = now,
                ExpiresAt = expires
            };
        }

        private string CreateRefreshToken(IEnumerable<Claim> claims)
        {
            var handler = new JwtSecurityTokenHandler();
            var refreshToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = JwtTokenDefinitions.Issuer,
                Audience = JwtTokenDefinitions.Audience,
                SigningCredentials = JwtTokenDefinitions.SigningCredentials,
                Subject =  new ClaimsIdentity(claims),
                Expires = DateTime.Now.Add(JwtTokenDefinitions.RefreshTokenExpirationTime),
                NotBefore = DateTime.Now
            });

            return handler.WriteToken(refreshToken);
        }
    }
}