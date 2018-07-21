using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.Repository.Contract;
using SocialNetwork.Repository.Contract.Models;
using SocialNetwork.Repository.Repository;

namespace SocialNetwork.Repository.DI
{
    public static class DataAcessServiceRegister
    {
        public static IServiceCollection RegisterDataAccessServices(this IServiceCollection serviceCollection, string connectionString)
        {
           return serviceCollection
               .AddScoped<IRepository<User>>(provider => new UserRepository(connectionString))
               .AddScoped<IRepository<Role>>(provider => new RoleRepository());
        }
    }
}