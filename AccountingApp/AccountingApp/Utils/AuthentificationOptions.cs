using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace AccountingApp.Utils
{
    public class AuthentificationOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { private get; set; }
        /// <summary>
        /// Lifetime of token in minutes.
        /// </summary>
        public int Lifetime { get; set; }
        public bool ValidateIssuer => Issuer is not null;
        public bool ValidateAudience => Audience is not null;
        public bool ValidateLifeTime => Lifetime != 0;
        public bool ValidateSigningKey => SigningKey is not null;
        public SymmetricSecurityKey SigningKey { get; private set; }

        public void GenerateKey()
        {
            if (Key is not null)
            {
                SigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
                return;
            }
            var randomBytes = new byte[16];
            RandomNumberGenerator.Create().GetBytes(randomBytes);
            SigningKey = new SymmetricSecurityKey(randomBytes);
        }
    }
}
