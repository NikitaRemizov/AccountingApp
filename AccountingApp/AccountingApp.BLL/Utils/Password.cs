using System;
using System.Linq;
using System.Security.Cryptography;

namespace AccountingApp.BLL.Utils
{
    public class Password
    {
        public const int HashSize = 20;
        public const int SaltSize = 16;
        public const int StoredHashSize = HashSize + SaltSize;
        private const int NumberOfHashInterations = 10000;

        public byte[] Hash => _salt.Concat(_hash).ToArray();

        private byte[] _hash;
        private byte[] _salt;

        public Password(string password)
        {
            _salt = new byte[SaltSize];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(_salt);

            using var deriveBytes = new Rfc2898DeriveBytes(password, _salt, NumberOfHashInterations);
            _hash = deriveBytes.GetBytes(HashSize);
        }

        public Password(string password, byte[] hashToGetSaltFrom)
        {
            _salt = GetSalt(hashToGetSaltFrom);
            using var deriveBytes = new Rfc2898DeriveBytes(password, _salt, NumberOfHashInterations);
            _hash = deriveBytes.GetBytes(HashSize);
        }

        private static byte[] GetSalt(byte[] storedHash)
        {
            if (storedHash.Length != StoredHashSize)
            {
                throw new ArgumentException(
                    $"Incorrect stored hash size, expected: {StoredHashSize}, actual: {storedHash.Length}", nameof(storedHash));
            }
            var salt = new byte[SaltSize];
            Array.Copy(storedHash, 0, salt, 0, SaltSize);
            return salt;
        }
    }
}
