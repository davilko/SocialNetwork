using System;
using Microsoft.AspNetCore.Http;

namespace SocialNetwork.Security.Configuration
{
    public interface ICookieManager
    {
        void SetCookie<T>(HttpContext context, string key, T value, DateTime? expires);
        T GetCookie<T>(HttpContext context, string key);
    }
}