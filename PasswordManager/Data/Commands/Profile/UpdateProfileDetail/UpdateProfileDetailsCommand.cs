using PasswordManager.Logic.Profile;

namespace PasswordManager.Data.Commands.Profile.UpdateProfileDetail {
    public class UpdateProfileDetailsCommand : ISeparatedCommand {
        private int _accountId;
        private ProfileDetailViewModel _viewModel;

        public UpdateProfileDetailsCommand(int accountId, ProfileDetailViewModel viewModel) {
            _accountId = accountId;
            _viewModel = viewModel;
        }

        public int AccountId { get { return _accountId; } set { _accountId = value; } }

        public ProfileDetailViewModel ViewModel { get { return _viewModel; } set { _viewModel = value; } }
    }
}
