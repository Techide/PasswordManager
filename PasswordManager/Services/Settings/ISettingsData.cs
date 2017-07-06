namespace PasswordManager.Services.Settings {

    internal interface ISettingsData {
        byte[] Data { get; set; }
        byte[] IV { get; set; }
        byte[] Salt { get; set; }
    }
}