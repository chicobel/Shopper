using Shopper.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shopper.Auth
{
    public class Token
    {
        public string UserId { get; set; }

        public DateTime Expiry { get; set; }
        public Token(string userid, DateTime  expiry)
        {
            UserId = userid;
            Expiry = expiry;
        }

        public string Encrypt()
        {
            var cryptoHelper = new CryptoHelper();
            var cert = cryptoHelper.GetX509Certificate("CN=WebAPI-Token");
            return cryptoHelper.Encrypt(cert, this.ToString());
        }

        public override string ToString()
        {
            return String.Format("UserId={0};Expiry={1}", this.UserId, this.Expiry.ToString("dd/MM/yyyy"));
        }

        public static Token Decrypt(string encryptedToken)
        {
            var cryptoHelper = new CryptoHelper();
            var cert = cryptoHelper.GetX509Certificate("CN=WebAPI-Token");
            string decrypted = cryptoHelper.Decrypt(cert, encryptedToken);
            Dictionary<string, string> dictionary = decrypted.ToDictionary();
            DateTime d;
            DateTime.TryParse(dictionary["Expiry"], out d);
            return new Token(dictionary["UserId"],d);
        }

    }
}