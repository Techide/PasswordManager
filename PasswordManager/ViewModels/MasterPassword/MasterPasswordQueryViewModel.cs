using PasswordManager.Data.Queries;
using PasswordManager.Models.Data.Queries.MasterPassword.AuthenticateMasterPassword;
using PasswordManager.Services.Navigation;
using PasswordManager.Services.Settings;
using PasswordManager.Util.MVVM;
using Windows.UI.Xaml.Input;
using Windows.System;

namespace PasswordManager.ViewModels {

    public class MasterPasswordQueryViewModel : ABindableBase, IViewModel {
        private readonly ISeparatedQueryHandler<AuthenticateMasterPasswordQuery, AuthenticateMasterPasswordResult> _authenticateHandler;

        public string Password { get; set; }

        public MasterPasswordQueryViewModel(ISeparatedQueryHandler<AuthenticateMasterPasswordQuery, AuthenticateMasterPasswordResult> authenticateHandler) {
            Password = string.Empty;
            _authenticateHandler = authenticateHandler;
            PasswordBoxPasswordChangedCommand = new DelegateCommand(PasswordBoxPasswordChangedCommandExecute);
            PasswordBoxKeyDownCommand = new DelegateCommand(PasswordBoxKeyDownCommandExecute, PasswordBoxKeyDownCommandCanExecute);
        }

        private void PasswordBoxPasswordChangedCommandExecute() {
        }

        private bool PasswordBoxKeyDownCommandCanExecute(object obj) {
            var data = obj as KeyRoutedEventArgs;
            if (data != null) {
                return data.Key == VirtualKey.Enter;
            }
            return false;
        }

        private void PasswordBoxKeyDownCommandExecute() {
            AuthenticateMasterPassword();
        }

        private void AuthenticateMasterPassword() {
            if (_authenticateHandler.Execute(new AuthenticateMasterPasswordQuery(Password)).Authenticated) {
                AppSettings.MasterPassword = Password;
                NavigationService.Navigate(typeof(MainPageViewModel));
            }
        }

        public DelegateCommand PasswordBoxPasswordChangedCommand { get; set; }

        public DelegateCommand PasswordBoxKeyDownCommand { get; set; }
    }
}