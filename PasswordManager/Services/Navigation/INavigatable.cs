using PasswordManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Services.Navigation {

    public interface INavigatable<T> where T : IViewModel {

        event EventHandler<IEventArgumentWrapper> OnNavigatedTo;
    }
}