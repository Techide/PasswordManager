using PasswordManager.Util.MVVM;

namespace PasswordManager.Models.Data.Queries.MasterPassword.AuthenticateMasterPassword {

    public class AuthenticateMasterPasswordResult : ABindableBase {
        public bool Authenticated { get; internal set; }
    }
}