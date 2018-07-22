using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SocialNetwork.Configuration;

namespace SocialNetwork.Security.Configuration
{
    public static class JwtConfigure
    {
        public static void ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        { 
            
            JwtTokenDefinitions.LoadFromConfiguration(configuration);
            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        IssuerSigningKey = JwtTokenDefinitions.IssuerSigningKey,

                        ValidAudience = JwtTokenDefinitions.Audience,

                        ValidIssuer = JwtTokenDefinitions.Issuer,

                        ValidateIssuerSigningKey = JwtTokenDefinitions.ValidateIssuerSigningKey,

                        ValidateLifetime = JwtTokenDefinitions.ValidateLifetime,

                        ClockSkew = JwtTokenDefinitions.ClockSkew
                    };
                }
            );

            services.AddSingleton<ITokenManager, TokenManager>();
        }
    }
}
