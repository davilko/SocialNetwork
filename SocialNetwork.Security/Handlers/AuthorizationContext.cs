using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace SocialNetwork.Security.Handlers
{
    public class AuthorizationContext
    {
        public bool IsAuthrize => _isSuccess;
        public bool IsFaild => !_isSuccess;

        public ClaimsPrincipal User { get; }
        public IAuthorizationRequirement Requirement { get; set; }
        
        private bool _isSuccess = false;

        public AuthorizationContext(
            IAuthorizationRequirement requirement, 
            ClaimsPrincipal user)
        {
            Requirement = requirement;
            User = user;
        }

        public void Success()
        {
            _isSuccess = true;
        }

        public void Fail()
        {
            _isSuccess = false;
        }
    }
}