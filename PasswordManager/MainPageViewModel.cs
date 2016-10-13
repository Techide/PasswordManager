using PasswordManager.Logic;
using PasswordManager.Logic.Profile;
using PasswordManager.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager {
    public class MainPageViewModel : IViewModel<MainPageViewModel> {

        public MainPageViewModel(IViewModel<ProfileListViewModel> userList) {
            ProfileListViewModel = userList;
        }

        public IViewModel<ProfileListViewModel> ProfileListViewModel { get; private set; }

    }
}
