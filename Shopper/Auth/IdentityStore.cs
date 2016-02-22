using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shopper.Auth
{
    public class IdentityStore
    {
       public static Dictionary<string, string> logins = new Dictionary<string, string>() { { "myusername", "checkout.com" } };

       public static bool IsValidUserId(string userid)
       {
           return logins.ContainsKey(userid);
       }
    }
}