using PasswordManager.Logic.Profile;
using SimpleInjector;
using System.Reflection;

namespace PasswordManager.Logic {
    public class ViewModelLocator {
        private static Container _ioc;

        public ViewModelLocator() {
            _ioc = new Container();
            Bootstrap();
        }

        public MainPageViewModel MainPage { get { return _ioc.GetInstance<MainPageViewModel>(); } }

        public ProfileListViewModel ProfileListViewModel { get { return _ioc.GetInstance<ProfileListViewModel>(); } }

        private static void Bootstrap() {
            //_ioc.Register(typeof(IViewModel<>), new[] { typeof(IViewModel<>).GetTypeInfo().Assembly });
            _ioc.Register<MainPageViewModel>();
            _ioc.Register<ProfileListViewModel>();

        }
    }
}
