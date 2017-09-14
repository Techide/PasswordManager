using PasswordManager.Services.Navigation;
using PasswordManager.Services.Settings;
using PasswordManager.Util.MVVM;
using Windows.UI.Xaml.Input;
using Windows.System;
using PasswordManager.Models.Data.Queries;

namespace PasswordManager.ViewModels {

    public class MasterPasswordQueryViewModel : ABindableBase, IRootViewModel, IViewModel {
        private readonly ISeparatedQueryHandler<AuthenticateMasterPasswordQuery, bool> _authenticateHandler;
        private NavigationService _navigation;

        public string Password { get; set; }

        public MasterPasswordQueryViewModel(NavigationService navigation, ISeparatedQueryHandler<AuthenticateMasterPasswordQuery, bool> authenticateHandler) {
            _navigation = navigation;
            Password = string.Empty;
            _authenticateHandler = authenticateHandler;
            PasswordBoxKeyDownCommand = new DelegateCommand(PasswordBoxKeyDownCommandExecute, PasswordBoxKeyDownCommandCanExecute);
        }

        private bool PasswordBoxKeyDownCommandCanExecute(object obj) {
            var keyEvent = obj as KeyRoutedEventArgs;

            if (obj == null) {
                return false;
            }

            if (keyEvent != null) {
                return keyEvent.Key == VirtualKey.Enter;
            }
            return false;
        }

        private void PasswordBoxKeyDownCommandExecute() {
            if (_authenticateHandler.Execute(new AuthenticateMasterPasswordQuery(Password))) {
                AppSettings.MasterPassword = Password;
                _navigation.Navigate(typeof(NavigationMenuViewModel));
            }
        }

        public DelegateCommand PasswordBoxKeyDownCommand { get; set; }
    }
}