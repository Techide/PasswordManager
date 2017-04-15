using PasswordManager.Data.Queries;

namespace PasswordManager.Models.Data.Queries.MasterPassword.AuthenticateMasterPassword {

    public class AuthenticateMasterPasswordQuery : ISeparatedQuery<AuthenticateMasterPasswordResult> {

        public AuthenticateMasterPasswordQuery(string password) {
            Password = password;
        }

        public string Password { get; internal set; }
    }
}