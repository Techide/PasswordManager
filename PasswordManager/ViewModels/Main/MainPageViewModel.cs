using MetroLog;

namespace PasswordManager.ViewModels {

    public class MainPageViewModel : IViewModel {
        private ILogger Log = LogManagerFactory.DefaultLogManager.GetLogger<MainPageViewModel>();

        public MainPageViewModel(ProfileListViewModel userList) {
            ProfileListViewModel = userList;
        }

        public ProfileListViewModel ProfileListViewModel { get; private set; }
    }
}