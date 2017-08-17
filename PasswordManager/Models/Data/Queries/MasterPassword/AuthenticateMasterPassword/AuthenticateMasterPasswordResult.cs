using PasswordManager.Util.MVVM;

namespace PasswordManager.Models.Data.Queries {

    public class AuthenticateMasterPasswordResult : ABindableBase {
        public bool Authenticated { get; internal set; }
    }
}