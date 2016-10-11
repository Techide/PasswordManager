using PasswordManager.Logic.Profile;
using PasswordManager.Presentation;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Logic {
    public class ViewModelLocator {
        private static Container _ioc;

        public ViewModelLocator() {
            Bootstrap();
        }

        public ProfileListViewModel ProfileList { get { return _ioc.GetInstance<ProfileListViewModel>(); } }

        private static void Bootstrap() {
            var container = new Container();

            container.Register(typeof(IViewModel<>), new[] { typeof(IViewModel<>).GetTypeInfo().Assembly });

            _ioc = container;

        }
    }
}
