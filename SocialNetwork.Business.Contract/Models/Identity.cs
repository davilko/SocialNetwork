using System;
using System.Collections.Generic;

namespace SocialNetwork.Business.Contract.Models
{
    public class Identity
    {
        public Guid UserId { get; set; }
        public List<string> Roles { get; set; }
    }
}