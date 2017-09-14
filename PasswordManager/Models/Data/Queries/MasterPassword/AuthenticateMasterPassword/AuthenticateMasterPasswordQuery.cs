using PasswordManager.Services.Settings;

namespace PasswordManager.Models.Data.Queries {

    public class AuthenticateMasterPasswordQuery : ISeparatedQuery<bool> {

        public AuthenticateMasterPasswordQuery(string password) {
            Password = password ?? string.Empty;
            MasterPasswordKey = AppSettings.MASTER_PASSWORD_KEY;
        }

        public string MasterPasswordKey { get; set; }

        public string Password { get; internal set; }
    }
}