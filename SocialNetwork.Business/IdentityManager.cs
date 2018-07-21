using System;
using System.Linq;
using System.Threading.Tasks;
using SocialNetwork.Business.Contract;
using SocialNetwork.Business.Contract.Models;
using SocialNetwork.Repository.Contract;
using SocialNetwork.Repository.Contract.Models;
using SocialNetwork.Repository.Spicification;
using SocialNetwork.Utility.Cryptography;

namespace SocialNetwork.Business
{
    public class IdentityManager : IIdentityManager
    {
        private readonly IRepository<User> _userRepository; 
        private readonly IRepository<Role> _roleRepository;

        public IdentityManager(
            IRepository<User> userRepository, 
            IRepository<Role> roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<Identity> GetUserIdentity(string login, string password)
        {
           var identityUser = await _userRepository.Get(new UserSpecification(user
                => user.Login == login && user.Password == password.ToSHA256()));

            var roles = await _roleRepository.Get(null);

            return new Identity
            {
                UserId = identityUser.First().Id,
                Roles = roles.Select(r => r.Name).ToList()
            };
        }
    }
}