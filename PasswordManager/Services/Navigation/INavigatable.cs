using PasswordManager.ViewModels;
using System;

namespace PasswordManager.Services.Navigation {

    public interface INavigatable<T> where T : IViewModel {

        event EventHandler<IEventArgumentWrapper> OnNavigatedTo;
    }
}