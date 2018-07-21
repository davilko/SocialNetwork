using System;
using System.Linq.Expressions;
using SocialNetwork.Repository.Contract;
using SocialNetwork.Repository.Contract.Models;

namespace SocialNetwork.Repository.Spicification
{
    public class UserSpecification : BaseSpecification<User>
    {
        public UserSpecification(Expression<Func<User, bool>> predicate) 
            : base(predicate)
        {
            
        }
    }
}