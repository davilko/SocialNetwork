using System;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SocialNetwork.Security.Configuration;

namespace SocialNetwork.Security
{
    public class CookieManager : ICookieManager
    {
        public void SetCookie<T>(HttpContext context, string key, T value, DateTime? expires)
        {
            var valueString = JsonConvert.SerializeObject(value);
            
            context.Response.Cookies.Append(
                key, 
                valueString, 
                new CookieOptions{ Secure = true, Expires = expires});
        }

        public T GetCookie<T>(HttpContext context, string key)
        {
            var value = context.Request.Cookies[key];
            return JsonConvert.DeserializeObject<T>(value);
        }
    }

    public static class Cookie
    {
        public static string AccessToken => "access_token";
        public static string RefreshToken => "refresh_token";
    }
}