using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Xamine.Models
{
    public class CookieStore
    {
        public static void SetCookie(string key, string value)
        {
            HttpCookie cookie = new HttpCookie(key, value);
            if (HttpContext.Current.Request.Cookies[key] != null)
            {
                var cookieOld = HttpContext.Current.Request.Cookies[key];
                cookieOld.Expires = DateTime.Now.AddMinutes(30);
                cookieOld.Value = cookie.Value;
                HttpContext.Current.Response.Cookies.Add(cookieOld);
            }
            else
            {
                cookie.Expires = DateTime.Now.AddMinutes(30);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
        public static string GetCookie(string key)
        {
            string value = string.Empty;
            HttpCookie cookie = HttpContext.Current.Request.Cookies[key];

            if (cookie != null)
            {
                // For security purpose, we need to encrypt the value.
                value = cookie.Value;
            }
            return value;
        }

        public static void RemoveCookie(string key)
        {
            
            if (HttpContext.Current.Request.Cookies[key] != null)
            {
                var cookie = HttpContext.Current.Request.Cookies[key];
                cookie.Expires = DateTime.Now.AddMinutes(-30);
                
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            
        }


    }
}