using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Shopper.Auth
{
    public class CustAuthorizeAttribute: AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var reqAuth = actionContext.Request.Headers.Authorization.ToString();
           // if()
            try
            {
                reqAuth = reqAuth.Replace("Bearer ","");
                var token = Token.Decrypt(reqAuth);
                bool isValidUserId = IdentityStore.IsValidUserId(token.UserId);
                bool tokenNotExpired = DateTime.Now <= token.Expiry;
                return isValidUserId && tokenNotExpired;
            }
            catch (Exception e)
            {
                return false;
            }
            //return base.IsAuthorized(actionContext);
        }
    }
}