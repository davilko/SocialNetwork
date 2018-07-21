using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialNetwork.Repository.Contract;
using SocialNetwork.Repository.Contract.Models;

namespace SocialNetwork.Repository.Repository
{
    public class UserRepository : IRepository<User>
    {
        private readonly string _connectionString;
        
        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public Task<User> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Create(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> Get(ISpecification<User> specification)
        {
            return Task.FromResult((IEnumerable<User>) new []
            {
                new User {Email = "razz2zz", Login = "davilko", Name = "vlad", Id = Guid.NewGuid()}
            });
        }
    }
}