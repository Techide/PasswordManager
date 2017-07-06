using System;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;

namespace PasswordManager.Services.Cryptography {

    internal class Cryptographer {

        // Derivation
        private static string _keyDerivationAlgorithmName;

        private static uint _iterationCount;

        // Encryption
        private static string _encryptionAlgorithmName;

        // Common
        private static uint _targetKeySize;

        private static BinaryStringEncoding _encoding;

        static Cryptographer() {
            _targetKeySize = 256;
            _encoding = BinaryStringEncoding.Utf16LE;

            _keyDerivationAlgorithmName = KeyDerivationAlgorithmNames.Pbkdf2Sha512;
            _iterationCount = 10000;

            _encryptionAlgorithmName = SymmetricAlgorithmNames.AesCbcPkcs7;
        }

        #region public

        public static CryptoPassword Encrypt(string password, string publicKeyPhrase) {
            var cp = new CryptoPassword { KeyPhrase = publicKeyPhrase };

            // Create a random Salt.
            cp.Salt = CreateSalt();

            // Create an Initialization Vector for our chosen algorithm.
            cp.IV = CreateInitializationVector();

            // Derive key material from the designated password-based key derivation function using the provided public key phrase.
            cp.KeyMaterial = DeriveKeyMaterial(cp);

            // Encrypt the password.
            cp.EncryptedPassword = EncryptPassword(password, cp);

            return cp;
        }

        public static string Decrypt(CryptoPassword cp) {
            cp.KeyMaterial = DeriveKeyMaterial(cp);
            return DecryptPassword(cp);
        }

        private static byte[] CreateSalt() {
            IBuffer bufferedSalt = CryptographicBuffer.GenerateRandom(256);
            return CopyBufferToByteArray(bufferedSalt);
        }

        private static byte[] CreateInitializationVector() {
            // Open a symmetric algorithm provider for the specified algorithm.
            var provider = SymmetricKeyAlgorithmProvider.OpenAlgorithm(_encryptionAlgorithmName);

            // Create an Initialization Vector for algorithm.
            var bufferIV = CryptographicBuffer.GenerateRandom(provider.BlockLength);

            return CopyBufferToByteArray(bufferIV);
        }

        private static byte[] EncryptPassword(string password, CryptoPassword cp) {
            // Create a buffer that contains the encoded password to be encrypted.
            IBuffer bufferedPassword = CryptographicBuffer.ConvertStringToBinary(password, _encoding);

            // Restore Initialization Vector as buffer.
            IBuffer bufferIV = CryptographicBuffer.CreateFromByteArray(cp.IV);

            // Restore KeyMaterial and create symmetric key
            var bufferedKey = CryptographicBuffer.CreateFromByteArray(cp.KeyMaterial);
            var key = CreateSymmetricKey(bufferedKey);

            // Encrypt the password
            IBuffer bufferEncrypted = CryptographicEngine.Encrypt(key, bufferedPassword, bufferIV);

            // Convert the buffered encryption and return it.
            byte[] result;
            //CryptographicBuffer.EncodeToBase64String(bufferEncrypted);
            CryptographicBuffer.CopyToByteArray(bufferEncrypted, out result);
            return result;
        }

        private static byte[] DeriveKeyMaterial(CryptoPassword cp) {
            // Open the specified algorithm.
            var provider = KeyDerivationAlgorithmProvider.OpenAlgorithm(_keyDerivationAlgorithmName);

            // Create a buffer that contains the keyPhrase for use during derivation.
            IBuffer bufferedSecret = CryptographicBuffer.ConvertStringToBinary(cp.KeyPhrase, _encoding);

            // Restore salt bytes as buffer.
            var bufferedSalt = CryptographicBuffer.CreateFromByteArray(cp.Salt);

            // Create the required derivation parameters.
            var pbkdf2Parameter = KeyDerivationParameters.BuildForPbkdf2(bufferedSalt, _iterationCount);

            // Create a key from the buffered secret.
            var keyOriginal = provider.CreateKey(bufferedSecret);

            // Derive a key from the original created key and the derivation parameters.
            var keyMaterial = CryptographicEngine.DeriveKeyMaterial(keyOriginal, pbkdf2Parameter, _targetKeySize);

            return CopyBufferToByteArray(keyMaterial);
        }

        private static CryptographicKey CreateSymmetricKey(IBuffer bufferedKeyMaterial) {
            var provider = SymmetricKeyAlgorithmProvider.OpenAlgorithm(_encryptionAlgorithmName);
            return provider.CreateSymmetricKey(bufferedKeyMaterial);
        }

        private static CryptographicKey CreateDerivedKey(IBuffer bufferedKeyMaterial) {
            var provider = KeyDerivationAlgorithmProvider.OpenAlgorithm(_keyDerivationAlgorithmName);
            return provider.CreateKey(bufferedKeyMaterial);
        }

        private static byte[] CopyBufferToByteArray(IBuffer data) {
            byte[] result;
            CryptographicBuffer.CopyToByteArray(data, out result);
            return result;
        }

        private static string DecryptPassword(CryptoPassword cp) {
            // Restore Buffer for IV.
            var bufferIV = CryptographicBuffer.CreateFromByteArray(cp.IV);

            // Restore Buffer for Encrypted Password.
            var bufferEncryptedPassword = CryptographicBuffer.CreateFromByteArray(cp.EncryptedPassword);

            // Restore Buffer for Key.
            var bufferKeyMaterial = CryptographicBuffer.CreateFromByteArray(cp.KeyMaterial);

            // Rebuild key.
            var key = CreateSymmetricKey(bufferKeyMaterial);

            // Get buffer for decrypted Password.
            var bufferDecrypt = CryptographicEngine.Decrypt(key, bufferEncryptedPassword, bufferIV);

            return CryptographicBuffer.ConvertBinaryToString(_encoding, bufferDecrypt);
        }

        #endregion public
    }
}