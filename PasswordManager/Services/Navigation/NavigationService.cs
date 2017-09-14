using PasswordManager.Services.Frames;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PasswordManager.Services.Navigation {

    /// <summary>
    /// Handles all navigation and mapping between views and viewmodels by self-registering unmapped views.
    /// </summary>
    /// 
    /// <remarks>
    /// Views and ViewModels must adhere to the specified convention:
    ///     View:   
    ///             viewname suffixed with Page.
    ///             Must live in the namespace of PasswordManager.Views
    ///     ViewModel:
    ///             modelname suffixed with ViewModel.
    ///             Must live in the namespace of PasswordManager.ViewModels
    /// </remarks>
    public class NavigationService : INavigationService {

        private List<Navigation> _mapping;
        private FrameService _frameService;
        private const string viewModelNameSpaceName = "PasswordManager.ViewModels";
        private const string pageNameSpace = "PasswordManager.Views";

        public NavigationService(FrameService frameService) {
            _mapping = new List<Navigation>();
            _frameService = frameService;
        }

        private Navigation GetNavigation(Type viewModel) {
            var navigation = _mapping.Find(x => x.ViewModelType == viewModel);

            if (navigation == null) {
                navigation = Register(viewModel.Name);
            }

            return navigation;
        }

        private Navigation Register(string name) {

            Type viewModel = null;
            Type page = null;

            try {
                var strippedName = name.Split(new string[] { "ViewModel", "Page" }, StringSplitOptions.RemoveEmptyEntries).First();
                viewModel = Type.GetType(viewModelNameSpaceName + "." + strippedName + "ViewModel");
                page = Type.GetType(pageNameSpace + "." + strippedName + "Page");

                if (viewModel == null) {
                    throw new NullReferenceException("Type of ViewModel is null");
                }

                if (page == null) {
                    throw new NullReferenceException("Type of Page is null");
                }
            }
            catch (Exception ex) {
                throw;
            }

            var navigation = new Navigation(viewModel, page);

            if (!_mapping.Contains(navigation)) {
                _mapping.Add(navigation);
            }

            return navigation;
        }

        private void NavigateToViewModel(Navigation navigation) {
            var frame = _frameService.GetFrame(navigation.ViewModelType);
            frame.Navigate(navigation.PageType);
        }

        public void Navigate(Type viewModel) {
            var navigation = GetNavigation(viewModel);
            var frame = _frameService.GetFrame(navigation.ViewModelType);
            if (!frame.CurrentSourcePageType.Equals(navigation.PageType)) {
                frame.Navigate(navigation.PageType);
            }
        }

        public void Navigate(Type viewModel, dynamic argument) {
            var navigation = GetNavigation(viewModel);
            navigation.Argument = argument;
            var frame = _frameService.GetFrame(navigation.ViewModelType);
            frame.Navigate(navigation.PageType);
        }

        public void GoBack(Type viewModel) {
            var frame = _frameService.GetFrame(viewModel);
            if (frame.CanGoBack) {
                frame.GoBack();
            }
            else {
                Navigate(viewModel);
            }
        }

        public dynamic GetNavigationArgument(Type viewModel) {
            return _mapping.Find(x => x.ViewModelType == viewModel)?.Argument;
        }

    }

}

