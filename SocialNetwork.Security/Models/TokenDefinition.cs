using System;

namespace SocialNetwork.Business.Contract.Models
{
    public class TokenDefinition
    {
        public string AccessToken { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string RefreshToken { get; set; }
    }
}