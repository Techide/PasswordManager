using PasswordManager.ViewModels;
using PasswordManager.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace PasswordManager.Services.Navigation {

    public static class NavigationService {
        private static Dictionary<Type, Type> _mapping;
        private static Dictionary<Type, dynamic> _viewModelArgumentMapping;
        private static Frame _frame;

        static NavigationService() {
            _mapping = new Dictionary<Type, Type>();
            _viewModelArgumentMapping = new Dictionary<Type, dynamic>();

            _mapping[typeof(MainPageViewModel)] = typeof(MainPage);
            _mapping[typeof(CreateProfileViewModel)] = typeof(CreateProfilePage);
            _mapping[typeof(EditProfileViewModel)] = typeof(EditProfilePage);
            _frame = Window.Current.Content as Frame;
        }

        public static void GoBack() {
            if (_frame.CanGoBack) {
                _frame.GoBack();
            }
            else {
                _frame.Navigate(typeof(MainPage));
            }
        }

        public static void GoBack(Type viewModel) {
            if (_frame.CanGoBack) {
                _frame.GoBack();
            }
            else {
                Navigate(viewModel);
            }
        }

        public static void Navigate(Type viewModel) {
            var pageType = _mapping[viewModel];
            _frame.Navigate(pageType);
        }

        public static void Navigate(Type viewModelType, dynamic argument) {
            var pageType = _mapping[viewModelType];
            _viewModelArgumentMapping[viewModelType] = argument;
            _frame.Navigate(pageType, argument);
        }

        public static dynamic GetContext(Type viewModel) {
            var result = _viewModelArgumentMapping.SingleOrDefault(x => x.Key.Equals(viewModel));
            _viewModelArgumentMapping[viewModel] = null;
            return result.Value;
        }
    }
}