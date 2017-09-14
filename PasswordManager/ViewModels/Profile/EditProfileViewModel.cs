using PasswordManager.Models.Data.Commands;
using PasswordManager.Models.DTO;
using PasswordManager.Services.Navigation;
using PasswordManager.Util.MVVM;
using System;

namespace PasswordManager.ViewModels {

    public class EditProfileViewModel : ABindableBase, IViewModel {
        private ISeparatedCommandHandler<UpdateProfileCommand> _updateProfileHandler;
        private int _id;
        private string _profile;
        private string _account;
        private string _password;
        private ProfileDTO _dto;
        private INavigationService _navigation;

        public string Profile {
            get {
                return _profile;
            }
            set {
                Set(ref _profile, value);
                UpdateCommand.RaiseCanExecuteChanged();
            }
        }

        public string Account {
            get { return _account; }
            set {
                Set(ref _account, value);
                UpdateCommand.RaiseCanExecuteChanged();
            }
        }

        public string Password {
            get { return _password; }
            set {
                Set(ref _password, value);
                UpdateCommand.RaiseCanExecuteChanged();
            }
        }

        public EditProfileViewModel(INavigationService navigation, ISeparatedCommandHandler<UpdateProfileCommand> updateProfileHandler) {
            _navigation = navigation;
            _updateProfileHandler = updateProfileHandler;
            UpdateCommand = new DelegateCommand(UpdateCommand_Execute, UpdateCommand_CanExecute);
            CancelCommand = new DelegateCommand(CancelCommand_Execute);

            _dto = _navigation.GetNavigationArgument(GetType()) as ProfileDTO;
            _id = _dto.ID;
            _profile = _dto.Profile;
            _account = _dto.Account;
            _password = _dto.Password;
        }

        public DelegateCommand UpdateCommand { get; set; }

        private bool UpdateCommand_CanExecute(object obj) {
            var canExecute = !string.IsNullOrWhiteSpace(_profile) && !string.IsNullOrWhiteSpace(_account) && !string.IsNullOrWhiteSpace(_password);
            if (canExecute) {
                canExecute = !_profile.Equals(_dto.Profile) || !_account.Equals(_dto.Account) || !_password.Equals(_dto.Password);
            }
            return canExecute;
        }

        private void UpdateCommand_Execute() {
            try {
                var command = new UpdateProfileCommand {
                    Id = _id,
                    Profile = _profile,
                    Account = _account,
                    Password = _password
                };
                _updateProfileHandler.Execute(command);
                _navigation.Navigate(typeof(MainViewModel), _id);
            }
            catch (Exception ex) {
                //Log.Error(string.Format("Failed to update profile details {0}", _profile), ex);
            }
        }

        public DelegateCommand CancelCommand { get; set; }

        private void CancelCommand_Execute() {
            _navigation.GoBack(typeof(MainViewModel));
        }
    }
}