﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Services.Cryptography {

    public sealed class SecurePasswordHasher {

        /// <summary>
        /// Size of salt
        /// </summary>
        private const int SaltSize = 256;

        /// <summary>
        /// Size of hash
        /// </summary>
        private const int HashSize = 512;

        private const int Iterations = 10000;

        /// <summary>
        /// Creates a hash from a password
        /// </summary>
        /// <param name="password">the password</param>
        /// <param name="iterations">number of iterations</param>
        /// <returns>the hash</returns>
        public static byte[] Hash(string password, int iterations) {
            //create salt
            byte[] salt = CreateSalt(SaltSize);

            //create hash
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            var hash = pbkdf2.GetBytes(HashSize);

            //combine salt and hash
            var hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            return hashBytes;

            //convert to base64
            //var base64Hash = Convert.ToBase64String(hashBytes);

            //format hash with extra information
            //return string.Format("$MYHASH$V1${0}${1}", iterations, base64Hash);
        }

        /// <summary>
        /// Creates a hash from a password with 10000 iterations
        /// </summary>
        /// <param name="password">the password</param>
        /// <returns>the hash</returns>
        public static byte[] Hash(string password) {
            return Hash(password, Iterations);
        }

        /// <summary>
        /// Check if hash is supported
        /// </summary>
        /// <param name="hashString">the hash</param>
        /// <returns>is supported?</returns>
        //public static bool IsHashSupported(string hashString) {
        //    return hashString.Contains("$MYHASH$V1$");
        //}

        /// <summary>
        /// verify a password against a hash
        /// </summary>
        /// <param name="password">the password</param>
        /// <param name="hashedPassword">the hash</param>
        /// <returns>could be verified?</returns>
        public static bool Verify(string password, byte[] hashedPassword) {
            //check hash
            //if (!IsHashSupported(hashedPassword)) {
            //    throw new NotSupportedException("The hashtype is not supported");
            //}

            //extract iteration and Base64 string
            //var splittedHashString = hashedPassword.Replace("$MYHASH$V1$", "").Split('$');
            //var iterations = int.Parse(splittedHashString[0]);
            //var base64Hash = splittedHashString[1];

            //get hashbytes
            //var hashBytes = Convert.FromBase64String(base64Hash);

            //get salt
            var salt = new byte[SaltSize];
            Array.Copy(hashedPassword, 0, salt, 0, SaltSize);

            //create hash with given salt
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            //get result
            for (var i = 0; i < HashSize; i++) {
                if (hashedPassword[i + SaltSize] != hash[i]) {
                    return false;
                }
            }
            return true;
        }

        private static byte[] CreateSalt(int size) {
            var result = Array.Empty<byte>();
            using (var rng = RandomNumberGenerator.Create()) {
                byte[] array = new byte[size];
                rng.GetBytes(array);
                result = array;
            }
            return result;
        }
    }
}