using PasswordManager.Services.Settings;
using System;
using System.IO;
using System.Security.Cryptography;

namespace PasswordManager.Services.Cryptography {

    internal class Cryptographer {

        #region Public

        public static CryptoPassword Encrypt(string password) {
            CryptoPassword result;
            using (var aes = Aes.Create()) {
                aes.Padding = PaddingMode.None;
                aes.GenerateIV();
                var cp = new CryptoPassword { PublicKey = SettingsService.Password };
                aes.Key = CreateKey(cp);
                cp.Salt = CreateSalt(256);
                cp.IV = aes.IV;
                cp.PrivateKey = aes.Key;
                cp.EncryptedPassword = EncryptPassword(password, cp.IV, cp.PrivateKey, cp.Salt);
                result = cp;
            }
            return result;
        }

        public static string Decrypt(CryptoPassword pe) {
            return DecryptPassword(pe);
        }

        #endregion Public

        #region Private

        private static byte[] EncryptPassword(string password, byte[] IV, byte[] Key, byte[] Salt) {
            byte[] result;
            using (var aes = Aes.Create()) {
                aes.Padding = PaddingMode.None;
                aes.Key = Key;
                aes.IV = IV;
                ICryptoTransform transform = aes.CreateEncryptor(aes.Key, aes.IV);
                using (MemoryStream memoryStream = new MemoryStream()) {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write)) {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream)) {
                            password += Convert.ToBase64String(Salt);
                            streamWriter.Write(password);
                        }
                        result = memoryStream.ToArray();
                    }
                }
            }
            return result;
        }

        private static string DecryptPassword(CryptoPassword cp) {
            string result;
            using (var aes = Aes.Create()) {
                aes.Padding = PaddingMode.None;
                aes.Key = CreateKey(cp);
                aes.IV = cp.IV;
                ICryptoTransform transform = aes.CreateDecryptor(aes.Key, aes.IV);
                using (MemoryStream memoryStream = new MemoryStream(cp.EncryptedPassword)) {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Read)) {
                        using (StreamReader streamReader = new StreamReader(cryptoStream)) {
                            var streamString = streamReader.ReadToEnd();
                            result = streamString.Replace(Convert.ToBase64String(cp.Salt), string.Empty);
                        }
                    }
                }
            }
            return result;
        }

        private static byte[] CreateKey(CryptoPassword cp) {
            var key = cp.PublicKey;
            var bytes = new Rfc2898DeriveBytes(key, cp.Salt, 4096);
            return bytes.GetBytes(32);
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

        #endregion Private
    }
}