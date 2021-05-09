using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BLL.Utils
{
    public class Password
    {
        private const int HashSize = 20;
        private const int SaltSize = 16;
        private const int NumberOfHashInterations = 10000;
        private const int StoredHashSize = HashSize + SaltSize;

        public byte[] Hash => _hash.ToArray();
        public byte[] Salt => _salt.ToArray();
        public byte[] StoredHash => _salt.Concat(_hash).ToArray();

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

        public bool IsMatching(Password password)
        {
            return _salt.SequenceEqual(password._salt) && _hash.SequenceEqual(password._hash);
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
