namespace PasswordManager.Services.Cryptography {

    internal class CryptoPassword {
        internal byte[] EncryptedPassword { get; set; }

        internal byte[] IV { get; set; }

        internal byte[] KeyMaterial { get; set; }

        internal byte[] Salt { get; set; }

        internal string KeyPhrase { get; set; }
    }
}