using PasswordManager.Models.Data.Commands;
using PasswordManager.Models.Data.Commands.MasterPassword;
using PasswordManager.Services.Navigation;
using PasswordManager.Services.Settings;
using PasswordManager.Util.MVVM;
using System;

namespace PasswordManager.ViewModels {

    public class MasterPasswordCreateViewModel : ABindableBase, IViewModel {
        private string _password;
        private string _verifyPassword;

        public string Password {
            get { return _password; }
            set {
                Set(ref _password, value);
                CreateMasterPasswordCommand.RaiseCanExecuteChanged();
            }
        }

        public string VerifyPassword {
            get { return _verifyPassword; }
            set {
                Set(ref _verifyPassword, value);
                CreateMasterPasswordCommand.RaiseCanExecuteChanged();
            }
        }

        private ISeparatedCommandHandler<CreateMasterPasswordCommand> _createMasterPasswordHandler;

        public MasterPasswordCreateViewModel(ISeparatedCommandHandler<CreateMasterPasswordCommand> createMasterPasswordHandler) {
            _createMasterPasswordHandler = createMasterPasswordHandler;
            CreateMasterPasswordCommand = new DelegateCommand(CreateMasterPassword, CreateMasterPasswordCheck);
        }

        private bool CreateMasterPasswordCheck(object obj) {
            var anyEmpty = (string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(VerifyPassword));
            if (anyEmpty) {
                return false;
            }
            return Password.Equals(VerifyPassword);
        }

        public DelegateCommand CreateMasterPasswordCommand { get; internal set; }

        private void CreateMasterPassword() {
            var command = new CreateMasterPasswordCommand { Password = Password };
            try {
                _createMasterPasswordHandler.Execute(command);
                AppSettings.MasterPassword = Password;
                NavigationService.Navigate(typeof(MainPageViewModel));
            }
            catch (Exception ex) {
                AppSettings.MasterPassword = string.Empty;
                throw;
            }
        }
    }
}