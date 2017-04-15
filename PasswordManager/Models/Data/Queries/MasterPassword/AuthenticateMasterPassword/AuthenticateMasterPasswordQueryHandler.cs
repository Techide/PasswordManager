using PasswordManager.Data.Queries;
using PasswordManager.Services.Settings;

namespace PasswordManager.Models.Data.Queries.MasterPassword.AuthenticateMasterPassword {

    public class AuthenticateMasterPasswordQueryHandler : ISeparatedQueryHandler<AuthenticateMasterPasswordQuery, AuthenticateMasterPasswordResult> {

        public AuthenticateMasterPasswordResult Execute(AuthenticateMasterPasswordQuery query) {
            SettingsService.Password = query.Password;
            return new AuthenticateMasterPasswordResult { Authenticated = true };
        }
    }
}