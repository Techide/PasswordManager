using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace PasswordManager.Services.Navigation {

    public class NavigationService : INavigationService {

        private List<Navigation> _mapping;
        private Frame _frame;

        public Frame Frame {
            get { return _frame; }
            set { _frame = value; }
        }

        public NavigationService() {
            _mapping = new List<Navigation>();
        }

        public void Register(Type viewModel, Type page) {
            var navigation = new Navigation(viewModel, page);
            if (!_mapping.Contains(navigation)) {
                _mapping.Add(navigation);
            }
        }

        public void Navigate(Type viewModel) {
            var navigation = _mapping.Find(x => x.ViewModelType == viewModel);
            _frame.Navigate(navigation.PageType);
        }

        public void Navigate(Type viewModel, dynamic argument) {
            _mapping.Find(x => x.ViewModelType == viewModel).Argument = argument;
            Navigate(viewModel);
        }

        public void GoBack(Type viewModel) {
            var navigation = _mapping.Find(x => x.ViewModelType == viewModel);
            if (_frame.CanGoBack) {
                _frame.GoBack();
            }
            else {
                Navigate(viewModel);
            }
        }

        public dynamic GetParameters(Type viewModel) {
            return _mapping.Find(x => x.ViewModelType == viewModel)?.Argument;
        }
    }

}

//namespace oldNavigation {
//    public static class NavigationService {
//        private static Dictionary<Type, Type> _mapping;
//        private static Dictionary<Type, dynamic> _viewModelArgumentMapping;
//        private static Frame _frame;

//        static NavigationService() {
//            _mapping = new Dictionary<Type, Type>();
//            _viewModelArgumentMapping = new Dictionary<Type, dynamic>();

//            _mapping[typeof(MainPageViewModel)] = typeof(MainPage);
//            _mapping[typeof(CreateProfileViewModel)] = typeof(CreateProfilePage);
//            _mapping[typeof(EditProfileViewModel)] = typeof(EditProfilePage);
//            _frame = Window.Current.Content as Frame;
//        }

//        public static void GoBack() {
//            if (_frame.CanGoBack) {
//                _frame.GoBack();
//            }
//            else {
//                _frame.Navigate(typeof(MainPage));
//            }
//        }

//        public static void GoBack(Type viewModel) {
//            if (_frame.CanGoBack) {
//                _frame.GoBack();
//            }
//            else {
//                Navigate(viewModel);
//            }
//        }

//        public static void Navigate(Type viewModel) {
//            var pageType = _mapping[viewModel];
//            _frame.Navigate(pageType);
//        }

//        public static void Navigate(Type viewModelType, dynamic argument) {
//            var pageType = _mapping[viewModelType];
//            _viewModelArgumentMapping[viewModelType] = argument;
//            _frame.Navigate(pageType, argument);
//        }

//        public static dynamic GetContext(Type viewModel) {
//            var result = _viewModelArgumentMapping.SingleOrDefault(x => x.Key.Equals(viewModel));
//            _viewModelArgumentMapping[viewModel] = null;
//            return result.Value;
//        }
//    }

//}