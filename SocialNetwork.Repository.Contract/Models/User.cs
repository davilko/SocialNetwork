using System;

namespace SocialNetwork.Repository.Contract.Models
{
    public class User
    {
        public string Name { get; set; }
        public Guid   Id { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}