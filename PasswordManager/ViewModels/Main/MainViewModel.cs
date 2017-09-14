using PasswordManager.Services.Navigation;
using PasswordManager.Util.MVVM;
using System.Linq;

namespace PasswordManager.ViewModels {

    public class MainViewModel : IViewModel {
        private INavigationService _navigation;

        public MainViewModel(INavigationService navigation, ProfileListViewModel userList) {
            _navigation = navigation;
            ProfileListViewModel = userList;
            LoadProfileListCommand = new DelegateCommand(LoadProfilesCommand_Execute);
        }

        public ProfileListViewModel ProfileListViewModel { get; private set; }

        public DelegateCommand LoadProfileListCommand { get; set; }

        private void LoadProfilesCommand_Execute() {
            var id = _navigation.GetNavigationArgument(GetType()) as int?;
            if (id.HasValue) {
                ProfileListViewModel.SelectedListItem = ProfileListViewModel.Profiles.SingleOrDefault(x => x.Id.Equals(id.Value));
            }
        }
    }
}