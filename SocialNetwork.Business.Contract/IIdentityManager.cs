using System.Threading.Tasks;
using SocialNetwork.Business.Contract.Models;

namespace SocialNetwork.Business.Contract
{
    public interface IIdentityManager
    {
        Task<Identity> GetUserIdentity(string login, string password);
    }
}