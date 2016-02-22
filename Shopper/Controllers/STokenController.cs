using Shopper.Auth;
using Shopper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Shopper.Controllers
{
    public class STokenController : ApiController
    {
       
        // GET: api/Token/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Token
        public IHttpActionResult Post(LoginModel m)
        {
            if (!IdentityStore.logins.ContainsKey(m.username))
                return BadRequest("Login not found");
            if (IdentityStore.logins[m.username] != m.password)
                return BadRequest("Incorrect password or username");
            var token = new Token(m.username, DateTime.Now.AddDays(21));
            return Ok(new SToken { Token=token.Encrypt(), Expiry= DateTime.UtcNow, Issued=DateTime.Now});
        }

    }
}
