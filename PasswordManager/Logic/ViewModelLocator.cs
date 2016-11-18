using PasswordManager.Data.Commands;
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
            _ioc.Register(typeof(ISeparatedQuery<>), new[] { typeof(ISeparatedQuery<>).GetTypeInfo().Assembly });
            _ioc.Register(typeof(ISeparatedQueryHandler<,>), new[] { typeof(ISeparatedQueryHandler<,>).GetTypeInfo().Assembly });

            _ioc.RegisterCollection(typeof(ISeparatedCommand), new[] { typeof(ISeparatedCommand).GetTypeInfo().Assembly });
            _ioc.Register(typeof(ISeparatedCommandHandler<>), new[] { typeof(ISeparatedCommandHandler<>).GetTypeInfo().Assembly });
        }
    }
}
