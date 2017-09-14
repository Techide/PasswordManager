using PasswordManager.Services.Navigation;
using PasswordManager.Util.MVVM;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using System;

namespace PasswordManager.ViewModels {
    public class NavigationMenuViewModel : ABindableBase, IRootViewModel, IViewModel {
        private List<MenuItem> _menuItems;
        private NavigationService _navigation;

        private static MenuItem _lastSelectedMenuItem;
        private static int _lastSelectedMenuItemIndex;

        public MenuItem LastSelectedMenuItem {
            get {
                return _lastSelectedMenuItem;
            }
            set {
                if (value != null && !value.ViewModelType.Equals(typeof(MasterPasswordQueryViewModel))) {
                    Set(ref _lastSelectedMenuItem, value);
                    LastSelectedMenuItemIndex = value.Index;
                }
            }
        }

        public int LastSelectedMenuItemIndex {
            get {
                return _lastSelectedMenuItemIndex != -1 ? _lastSelectedMenuItemIndex : -1;
            }
            set {
                Set(ref _lastSelectedMenuItemIndex, value);
            }
        }

        public List<MenuItem> MenuItems { get { return GetMenuItems(); } }

        public MenuItemDelegateCommand MenuItemClickedCommand { get; set; }

        public DelegateCommand HamburgerMenuLoadedCommand { get; set; }

        public NavigationMenuViewModel(NavigationService navigation) {
            _navigation = navigation;

            MenuItems.Add(new MenuItem {
                Icon = Symbol.List,
                Name = "Profiles",
                ViewModelType = typeof(MainViewModel)
            });

            MenuItems.Add(new MenuItem {
                Icon = Symbol.Setting,
                Name = "Settings",
                ViewModelType = typeof(SettingsViewModel)
            });

            MenuItems.Add(new MenuItem {
                Icon = Symbol.Permissions,
                Name = "Lock",
                ViewModelType = typeof(MasterPasswordQueryViewModel)
            });

            MenuItems.ForEach(x => { x.Index = _menuItems.IndexOf(x); });

            MenuItemClickedCommand = new MenuItemDelegateCommand(MenuItemClickedExecute);
            HamburgerMenuLoadedCommand = new DelegateCommand(HamburgerMenuLoadedCommandExecute);
        }

        private void HamburgerMenuLoadedCommandExecute() {
            LastSelectedMenuItem = _lastSelectedMenuItem;
            LastSelectedMenuItemIndex = _lastSelectedMenuItemIndex;
        }

        private void MenuOptionsItemClickedExecute(ItemClickEventArgs obj) {
            MenuItemClickedExecute(obj);
        }

        private void MenuItemClickedExecute(ItemClickEventArgs args) {
            var menuItem = args.ClickedItem as MenuItem;

            if (menuItem != null) {
                _navigation.Navigate(menuItem.ViewModelType);
            }
        }

        private List<MenuItem> GetMenuItems() {
            if (_menuItems == null) {
                _menuItems = new List<MenuItem>();
            }
            return _menuItems;
        }

    }
}
