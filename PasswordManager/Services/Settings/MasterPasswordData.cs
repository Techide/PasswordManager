namespace PasswordManager.Services.Settings {

    internal sealed class MasterPasswordData : ISettingsData {
        public byte[] Data { get; set; }
        public byte[] IV { get; set; }
        public byte[] Salt { get; set; }
    }
}