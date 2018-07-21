using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SocialNetwork.Repository.Contract;
using SocialNetwork.Repository.Contract.Models;

namespace SocialNetwork.Repository.Repository
{
    public class RoleRepository : IRepository<Role>
    {
        public Task<Role> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Role entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Create(Role entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Role>> Get(ISpecification<Role> specification)
        {
            return Task.FromResult((IEnumerable<Role>) new[] {new Role {Name = "Admin"}});
        }
    }
}