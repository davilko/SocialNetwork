using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNetwork.Repository.Contract
{
    public interface IRepository<TEntity>
    {
        Task<TEntity> Get(Guid id);
        Task<bool> Update(TEntity entity);
        Task<bool> Create(TEntity entity);
        Task<bool> Delete(Guid id);

        Task<IEnumerable<TEntity>> Get(ISpecification<TEntity> specification);
    }
}