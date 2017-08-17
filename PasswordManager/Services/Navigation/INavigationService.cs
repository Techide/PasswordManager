using System;
using PasswordManager.Models.DTO;

namespace PasswordManager.Services.Navigation {

    public interface INavigationService {

        void Navigate(Type navigatableViewModel);

        void Navigate(Type navigatableViewModel, dynamic parameters);

        void GoBack(Type viewModel);

        dynamic GetParameters(Type viewModel);

    }
}