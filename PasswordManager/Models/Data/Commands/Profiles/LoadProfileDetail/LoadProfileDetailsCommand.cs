using PasswordManager.ViewModels;

namespace PasswordManager.Models.Data.Commands {

    public class LoadProfileDetailsCommand : ISeparatedCommand {
        private int _accountId;
        private ProfileDetailViewModel _viewModel;

        public LoadProfileDetailsCommand(int accountId, ProfileDetailViewModel viewModel) {
            _accountId = accountId;
            _viewModel = viewModel;
        }

        public int AccountId { get { return _accountId; } set { _accountId = value; } }

        public ProfileDetailViewModel ViewModel { get { return _viewModel; } set { _viewModel = value; } }
    }
}