using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SocialNetwork.Configuration;
using SocialNetwork.Security.Middleware;

namespace SocialNetwork.Security.Configuration
{
    public static class JwtConfigure
    {
        public static void ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        { 
            
            JwtTokenDefinitions.LoadFromConfiguration(configuration);
            services.AddSingleton<ITokenManager, TokenManager>();
        }

        public static IApplicationBuilder UseJwtAuthentication(this IApplicationBuilder builder, string loginPath)
        {
            return builder.UseMiddleware<AuthenticationMiddleware>(loginPath);
        }
    }
}
