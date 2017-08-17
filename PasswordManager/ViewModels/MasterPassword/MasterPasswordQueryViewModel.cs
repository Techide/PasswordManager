using PasswordManager.Services.Navigation;
using PasswordManager.Services.Settings;
using PasswordManager.Util.MVVM;
using Windows.UI.Xaml.Input;
using Windows.System;
using PasswordManager.Models.Data.Queries;

namespace PasswordManager.ViewModels {

    public class MasterPasswordQueryViewModel : ABindableBase, IViewModel {
        private readonly ISeparatedQueryHandler<AuthenticateMasterPasswordQuery, AuthenticateMasterPasswordResult> _authenticateHandler;
        private NavigationService _navigation;

        public string Password { get; set; }

        public MasterPasswordQueryViewModel(NavigationService navigation, ISeparatedQueryHandler<AuthenticateMasterPasswordQuery, AuthenticateMasterPasswordResult> authenticateHandler) {
            _navigation = navigation;
            Password = string.Empty;
            _authenticateHandler = authenticateHandler;
            PasswordBoxKeyDownCommand = new DelegateCommand(PasswordBoxKeyDownCommandExecute, PasswordBoxKeyDownCommandCanExecute);
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
                _navigation.Navigate(typeof(MainPageViewModel));
            }
        }

        public DelegateCommand PasswordBoxKeyDownCommand { get; set; }
    }
}