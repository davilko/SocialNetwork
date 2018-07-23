using System.Collections.Generic;
using System.Security.Claims;
using SocialNetwork.Security.Models;

namespace SocialNetwork.Security
{
    public interface ITokenManager
    {
        TokenDefinition CreateTokenDefinition(IEnumerable<Claim> claims);
    }
}