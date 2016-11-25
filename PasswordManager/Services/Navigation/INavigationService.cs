using PasswordManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Services.Navigation {

    public interface INavigationService {

        void Navigate(Type navigatablePage);

        void Navigate(Type navigatablePage, object context);

        void GoBack();
    }
}