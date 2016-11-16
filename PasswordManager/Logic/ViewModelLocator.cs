using PasswordManager.Data.Queries;
using PasswordManager.Logic.Main;
using SimpleInjector;
using System.Diagnostics;
using System.Reflection;

namespace PasswordManager.Logic {
    public class ViewModelLocator {
        private static Container _ioc;

        public ViewModelLocator() {
            _ioc = new Container();
            Bootstrap();
            Debug.WriteLine("[Instantiated] ViewModelLocator");
        }

        public IViewModel<MainPageViewModel> MainPage { get { return _ioc.GetInstance<IViewModel<MainPageViewModel>>(); } }

        private static void Bootstrap() {
            _ioc.Register(typeof(IViewModel<>), new[] { typeof(IViewModel<>).GetTypeInfo().Assembly });
            _ioc.Register(typeof(IQuery<>), new[] { typeof(IQuery<>).GetTypeInfo().Assembly });
            _ioc.Register(typeof(IQueryHandler<,>), new[] { typeof(IQueryHandler<,>).GetTypeInfo().Assembly });
        }
    }
}
