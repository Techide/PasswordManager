using PasswordManager.Data.Queries;
using PasswordManager.Services.Settings;

namespace PasswordManager.Models.Data.Queries.MasterPassword.AuthenticateMasterPassword {

    public class AuthenticateMasterPasswordQuery : ISeparatedQuery<AuthenticateMasterPasswordResult> {

        public AuthenticateMasterPasswordQuery(string password) {
            Password = password ?? string.Empty;
            MasterPasswordKey = AppSettings.MASTER_PASSWORD_KEY;
        }

        public string MasterPasswordKey { get; set; }

        public string Password { get; internal set; }
    }
}