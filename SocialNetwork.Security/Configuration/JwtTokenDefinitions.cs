using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace SocialNetwork.Configuration
{
    public class JwtTokenDefinitions
    {
        public static void LoadFromConfiguration(IConfiguration configuration)
        {
            var config = configuration.GetSection("JwtConfiguration");
            Key = config.GetValue<string>("Key");
            Audience = config.GetValue<string>("Audience");
            Issuer = config.GetValue<string>("Issuer");
            TokenExpirationTime = TimeSpan.FromMinutes(config.GetValue<int>("TokenExpirationTime"));
            ValidateIssuerSigningKey = config.GetValue<bool>("ValidateIssuerSigningKey");
            ValidateLifetime = config.GetValue<bool>("ValidateLifetime");
            ClockSkew = TimeSpan.FromMinutes(config.GetValue<int>("ClockSkew"));
            RefreshTokenExpirationTime = TimeSpan.FromHours(config.GetValue<int>("RefreshTokenExpirationTime"));
        }

        public static string Key { get; set; } = string.Empty;

        public static SecurityKey IssuerSigningKey => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));

        public static SigningCredentials SigningCredentials => new SigningCredentials(IssuerSigningKey, SecurityAlgorithms.HmacSha256);

        public static TimeSpan TokenExpirationTime { get; set; } = TimeSpan.FromHours(1);
        
        public static TimeSpan RefreshTokenExpirationTime { get; set; } = TimeSpan.FromHours(60);

        public static TimeSpan ClockSkew { get; set; } = TimeSpan.FromHours(0);

        public static string Issuer { get; set; } = string.Empty;

        public static string Audience { get; set; } = string.Empty;

        public static bool ValidateIssuerSigningKey { get; set; } = true;

        public static bool ValidateLifetime { get; set; } = true;
    }
}