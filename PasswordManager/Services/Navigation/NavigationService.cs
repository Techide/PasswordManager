using PasswordManager.ViewModels;
using PasswordManager.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace PasswordManager.Services.Navigation {

    public sealed class NavigationService : INavigationService {
        private static volatile NavigationService _me;
        private static object syncRoot = new object();

        private Dictionary<Type, Type> _mapping;

        public static NavigationService Instance {
            get {
                if (_me == null) {
                    lock (syncRoot) {
                        if (_me == null) {
                            _me = new NavigationService();
                        }
                    }
                }
                return _me;
            }
        }

        private NavigationService() {
            _mapping = new Dictionary<Type, Type>();
            _mapping[typeof(MainPageViewModel)] = typeof(MainPage);
            _mapping[typeof(CreateProfileViewModel)] = typeof(CreateProfilePage);
            _mapping[typeof(EditProfileViewModel)] = typeof(EditProfilePage);
        }

        public void GoBack() {
            var frame = Window.Current.Content as Frame;
            if (frame.CanGoBack) {
                frame.GoBack();
            }
            else {
                frame.Navigate(typeof(MainPage));
            }
        }

        public void GoBack(Type viewModel) {
            var frame = Window.Current.Content as Frame;
            if (frame.CanGoBack) {
                frame.GoBack();
            }
            else {
                Navigate(viewModel);
            }
        }

        public void Navigate(Type viewModel) {
            var frame = Window.Current.Content as Frame;
            var pageType = _mapping[viewModel];
            frame.Navigate(pageType);
        }

        public void Navigate(Type viewModel, object context) {
            var frame = Window.Current.Content as Frame;
            var pageType = _mapping[viewModel];
            frame.Navigate(pageType, context);
        }
    }
}