using PasswordManager.Logic;
using PasswordManager.Logic.Profile;
using System.Diagnostics;

namespace PasswordManager.Logic.Main {
    public class MainPageViewModel : IViewModel<MainPageViewModel> {

        public MainPageViewModel(IViewModel<ProfileListViewModel> userList) {
            Debug.WriteLine("[Instantiated] MainPageViewModel");
            ProfileListViewModel = userList;
        }

        public IViewModel<ProfileListViewModel> ProfileListViewModel { get; private set; }

    }
}
