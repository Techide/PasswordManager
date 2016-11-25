using MetroLog;
using PasswordManager.Data.Queries;
using PasswordManager.Models.Data.Commands;
using SimpleInjector;
using System.Reflection;

namespace PasswordManager.ViewModels {

    public class ViewModelLocator {
        private static Container _ioc;
        private ILogger Log = LogManagerFactory.DefaultLogManager.GetLogger<ViewModelLocator>();

        public ViewModelLocator() {
            _ioc = new Container();
            Log.Trace("Bootstrapping SimpleInjector Container...");
            Bootstrap();
            Log.Trace("Bootstrapping SimpleInjector Container: Done");
        }

        public MainPageViewModel MainPage { get { return _ioc.GetInstance<MainPageViewModel>(); } }

        public CreateProfileViewModel CreateProfilePage { get { return _ioc.GetInstance<CreateProfileViewModel>(); } }

        public EditProfileViewModel EditProfilePage { get { return _ioc.GetInstance<EditProfileViewModel>(); } }

        private static void Bootstrap() {
            _ioc.RegisterCollection(typeof(IViewModel), new[] { typeof(IViewModel).GetTypeInfo().Assembly });
            _ioc.Register(typeof(ISeparatedQuery<>), new[] { typeof(ISeparatedQuery<>).GetTypeInfo().Assembly });
            _ioc.Register(typeof(ISeparatedQueryHandler<,>), new[] { typeof(ISeparatedQueryHandler<,>).GetTypeInfo().Assembly });

            _ioc.RegisterCollection(typeof(ISeparatedCommand), new[] { typeof(ISeparatedCommand).GetTypeInfo().Assembly });
            _ioc.Register(typeof(ISeparatedCommandHandler<>), new[] { typeof(ISeparatedCommandHandler<>).GetTypeInfo().Assembly });
        }
    }
}