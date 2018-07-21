using System.Linq.Expressions;

namespace SocialNetwork.Repository.Contract
{
    public interface ISpecification<TEntity>
    {
        Expression Expression { get; }
    }
}