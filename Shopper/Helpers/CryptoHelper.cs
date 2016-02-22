using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;

namespace Shopper.Helpers
{
    public class CryptoHelper
    {
        public X509Certificate2 GetX509Certificate(string subjectName)
        {
            var certStore = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            certStore.Open(OpenFlags.ReadOnly);
            X509Certificate2 certificate;

            try
            {
                certificate = certStore.Certificates.OfType<X509Certificate2>().
                                                                FirstOrDefault(cert => cert.SubjectName.Name.Equals(subjectName, StringComparison.OrdinalIgnoreCase));
            }
            finally
            {
                certStore.Close();
            }

            if (certificate == null)
                throw new Exception(String.Format("Certificate '{0}' not found.", subjectName));

            return certificate;
        }

        public string Encrypt(X509Certificate2 certificate, string plainToken)
        {
            var cryptoProvidor = (RSACryptoServiceProvider)certificate.PublicKey.Key;
            var encryptedTokenBytes = cryptoProvidor.Encrypt(Encoding.UTF8.GetBytes(plainToken), true);
            return Convert.ToBase64String(encryptedTokenBytes);
        }

        public string Decrypt(X509Certificate2 certificate, string encryptedToken)
        {
            var cryptoProvider = (RSACryptoServiceProvider)certificate.PrivateKey;
            var decryptedTokenBytes = cryptoProvider.Decrypt(Convert.FromBase64String(encryptedToken), true);

            return Encoding.UTF8.GetString(decryptedTokenBytes);
        }
    }
}
