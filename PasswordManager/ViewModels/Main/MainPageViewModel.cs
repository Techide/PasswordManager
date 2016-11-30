using MetroLog;
using PasswordManager.Services.Navigation;
using PasswordManager.Util.MVVM;
using System.Linq;

namespace PasswordManager.ViewModels {

    public class MainPageViewModel : IViewModel {
        private ILogger Log = LogManagerFactory.DefaultLogManager.GetLogger<MainPageViewModel>();

        public MainPageViewModel(ProfileListViewModel userList) {
            ProfileListViewModel = userList;
            LoadProfileListCommand = new DelegateCommand(LoadProfilesCommand_Execute);
        }

        public ProfileListViewModel ProfileListViewModel { get; private set; }

        public DelegateCommand LoadProfileListCommand { get; set; }

        private void LoadProfilesCommand_Execute(object argument) {
            var id = NavigationService.GetContext(GetType()) as int?;
            if (id.HasValue) {
                ProfileListViewModel.SelectedListItem = ProfileListViewModel.Profiles.SingleOrDefault(x => x.Id.Equals(id.Value));
            }
        }
    }
}