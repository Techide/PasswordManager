using MetroLog;
using PasswordManager.Models.Data.Commands;
using PasswordManager.Models.Data.Commands.Profiles.UpdateProfile;
using PasswordManager.Services.Navigation;
using PasswordManager.Util.MVVM;

namespace PasswordManager.ViewModels {

    public class EditProfileViewModel : ABindableBase, IViewModel {
        private ISeparatedCommandHandler<UpdateProfileCommand> _updateProfileHandler;
        private ILogger Log = LogManagerFactory.DefaultLogManager.GetLogger<EditProfileViewModel>();
        private int _id;
        private string _profile;
        private string _account;
        private string _password;

        public string Profile {
            get { return _profile; }
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

        public EditProfileViewModel(ISeparatedCommandHandler<UpdateProfileCommand> updateProfileHandler) {
            _updateProfileHandler = updateProfileHandler;
            UpdateCommand = new DelegateCommand(UpdateCommand_Execute, UpdateCommand_CanExecute);
            CancelCommand = new DelegateCommand(CancelCommand_Execute);
        }

        public DelegateCommand UpdateCommand { get; set; }

        private bool UpdateCommand_CanExecute(object obj) {
            return !string.IsNullOrWhiteSpace(_profile) && !string.IsNullOrWhiteSpace(_account) && !string.IsNullOrWhiteSpace(_password);
        }

        private void UpdateCommand_Execute(object obj) {
            // TODO: Get info.
            var command = new UpdateProfileCommand {
            };
        }

        public DelegateCommand CancelCommand { get; set; }

        private void CancelCommand_Execute(object parameter) {
            NavigationService.Instance.GoBack(typeof(MainPageViewModel));
        }
    }
}