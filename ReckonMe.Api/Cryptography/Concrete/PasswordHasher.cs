using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using ReckonMe.Api.Cryptography.Abstract;

namespace ReckonMe.Api.Cryptography.Concrete
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int SaltByteSize = 24;
        private const int HashByteSize = 24;
        private const int HashingIterationsCount = 10101;

        public string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }
            using (var bytes = new Rfc2898DeriveBytes(password, SaltByteSize, HashingIterationsCount))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(HashByteSize);
            }
            var dst = new byte[(SaltByteSize + HashByteSize) + 1];
            Buffer.BlockCopy(salt, 0, dst, 1, SaltByteSize);
            Buffer.BlockCopy(buffer2, 0, dst, SaltByteSize + 1, HashByteSize);
            return Convert.ToBase64String(dst);
        }

        public bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] passwordHashBytes;

            const int arrayLen = (SaltByteSize + HashByteSize) + 1;

            if (hashedPassword == null)
            {
                return false;
            }

            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            var src = Convert.FromBase64String(hashedPassword);

            if ((src.Length != arrayLen) || (src[0] != 0))
            {
                return false;
            }

            var currentSaltBytes = new byte[SaltByteSize];
            Buffer.BlockCopy(src, 1, currentSaltBytes, 0, SaltByteSize);

            var currentHashBytes = new byte[HashByteSize];
            Buffer.BlockCopy(src, SaltByteSize + 1, currentHashBytes, 0, HashByteSize);

            using (var bytes = new Rfc2898DeriveBytes(password, currentSaltBytes, HashingIterationsCount))
            {
                passwordHashBytes = bytes.GetBytes(SaltByteSize);
            }

            return AreHashesEqual(currentHashBytes, passwordHashBytes);
        }

        private static bool AreHashesEqual(IReadOnlyList<byte> firstHash, IReadOnlyList<byte> secondHash)
        {
            var minHashLength = firstHash.Count <= secondHash.Count ? firstHash.Count : secondHash.Count;
            var xor = firstHash.Count ^ secondHash.Count;
            for (var i = 0; i < minHashLength; i++)
                xor |= firstHash[i] ^ secondHash[i];
            return 0 == xor;
        }
    }
}