using System.Collections.Generic;
using System.Security.Claims;
using SocialNetwork.Business.Contract.Models;

namespace SocialNetwork.Security
{
    public interface ITokenManager
    {
        TokenDefinition CreateTokenDefinition(IEnumerable<Claim> claims);
    }
}