using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.Business.Contract;

namespace SocialNetwork.Business.DI
{
    public static class BusinessServiceRegister
    {
        public static IServiceCollection RegisterBusinessServices(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddTransient<IIdentityManager, IdentityManager>();
        }
    }
}