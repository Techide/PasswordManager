using PasswordManager.Services.Navigation;
using PasswordManager.Util.MVVM;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace PasswordManager.ViewModels {
    public class NavigationMenuViewModel : IViewModel {
        private List<MenuItem> _menuItems;
        private NavigationService _navigation;

        public NavigationMenuViewModel(NavigationService navigation) {
            _navigation = navigation;

            MenuItems.Add(new MenuItem {
                Icon = Symbol.List,
                Name = "Profiles",
                ViewModelType = typeof(MainPageViewModel)
            });

            MenuItems.Add(new MenuItem {
                Icon = Symbol.Permissions,
                Name = "Lock"
            });

            MenuItemClickedCommand = new MenuItemDelegateCommand(MenuItemClickedExecute);
            NavigateBackButtonClickedCommand = new DelegateCommand(NavigateBackwardsExecute, NavigateBackwardsCanExecute);
            NavigationFrameLoaded = new DelegateCommand<Frame>(NavigationFrameLoadedExecute);
        }

        private void NavigationFrameLoadedExecute(Frame frame) {
            _navigation.Frame = frame;
        }

        private bool NavigateBackwardsCanExecute(object obj) {
            return true;
        }

        private void NavigateBackwardsExecute() {
        }

        private void MenuItemClickedExecute(ItemClickEventArgs args) {
            var test = 0;
        }

        public List<MenuItem> MenuItems { get { return GetMenuItems(); } }

        private List<MenuItem> GetMenuItems() {
            if (_menuItems == null) {
                _menuItems = new List<MenuItem>();
            }

            return _menuItems;
        }

        public MenuItemDelegateCommand MenuItemClickedCommand { get; set; }

        public DelegateCommand NavigateBackButtonClickedCommand { get; set; }

        public DelegateCommand<Frame> NavigationFrameLoaded { get; set; }
    }
}
