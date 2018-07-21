using System;
using System.Linq.Expressions;
using SocialNetwork.Repository.Contract;

namespace SocialNetwork.Repository.Spicification
{
    public abstract class BaseSpecification<TEntity> : ISpecification<TEntity>
    {
        protected BaseSpecification(Expression<Func<TEntity, bool>> predicate)
        {
            Expression = predicate;
        }
        
        public Expression Expression { get; }
    }
}