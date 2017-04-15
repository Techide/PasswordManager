using PasswordManager.Data.Queries;
using PasswordManager.Models.Data.Queries.MasterPassword.AuthenticateMasterPassword;
using PasswordManager.Services.Navigation;
using PasswordManager.Services.Settings;
using PasswordManager.Util.MVVM;

namespace PasswordManager.ViewModels {

    public class MasterPasswordQueryViewModel : ABindableBase, IViewModel {
        private readonly ISeparatedQueryHandler<AuthenticateMasterPasswordQuery, AuthenticateMasterPasswordResult> _authenticateHandler;

        public string Password { get; set; }

        public MasterPasswordQueryViewModel(ISeparatedQueryHandler<AuthenticateMasterPasswordQuery, AuthenticateMasterPasswordResult> authenticateHandler) {
            _authenticateHandler = authenticateHandler;
            AuthenticateMasterPasswordCommand = new DelegateCommand(AuthenticateMasterPassword);
        }

        public void AuthenticateMasterPassword() {
            var result = _authenticateHandler.Execute(new AuthenticateMasterPasswordQuery(Password));
            if (result.Authenticated) {
                NavigationService.Navigate(typeof(MainPageViewModel));
            }
        }

        public DelegateCommand AuthenticateMasterPasswordCommand { get; set; }
    }
}