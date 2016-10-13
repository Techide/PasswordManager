using PasswordManager.Logic;
using PasswordManager.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager {
    public class MainPageViewModel : IPageViewModel<MainPage> {

        public MainPageViewModel(IViewModel<ProfileListUserControl> userList) {
            ProfileListViewModel = userList;
        }

        public IViewModel<ProfileListUserControl> ProfileListViewModel { get; private set; }

    }
}
