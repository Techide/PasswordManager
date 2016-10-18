namespace PasswordManager.Util.Crypto {
    internal class CryptoPassword {
        public byte[] EncryptedPassword {
            get;
            set;
        }

        public byte[] IV {
            get;
            set;
        }

        public byte[] PrivateKey {
            get;
            set;
        }

        public byte[] Salt {
            get;
            set;
        }

        public string PublicKey { get; set; }

    }
}
