using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace SocialNetwork.Security.Provider
{
    public class MinimumAgePolicyProvider : IAuthorizationPolicyProvider
    {
        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            throw new System.NotImplementedException();
        }

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}