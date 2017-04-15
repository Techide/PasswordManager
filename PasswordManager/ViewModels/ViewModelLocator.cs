using MetroLog;
using PasswordManager.Data.Queries;
using PasswordManager.Models.Data.Commands;
using SimpleInjector;
using System.Reflection;

namespace PasswordManager.ViewModels {

    public class ViewModelLocator {
        private static Container _ioc;
        private static ILogger Log = LogManagerFactory.DefaultLogManager.GetLogger(typeof(ViewModelLocator));

        static ViewModelLocator() {
            _ioc = new Container();
            Log.Trace("Bootstrapping SimpleInjector Container...");
            Bootstrap();
            Log.Trace("Bootstrapping SimpleInjector Container: Done");
        }

        public static MainPageViewModel MainPage { get { return _ioc.GetInstance<MainPageViewModel>(); } }

        public static CreateProfileViewModel CreateProfilePage { get { return _ioc.GetInstance<CreateProfileViewModel>(); } }

        public static EditProfileViewModel EditProfilePage { get { return _ioc.GetInstance<EditProfileViewModel>(); } }

        public static MasterPasswordQueryViewModel MasterPasswordPage { get { return _ioc.GetInstance<MasterPasswordQueryViewModel>(); } }

        private static void Bootstrap() {
            _ioc.RegisterCollection(typeof(IViewModel), new[] { typeof(IViewModel).GetTypeInfo().Assembly });
            _ioc.Register(typeof(ISeparatedQuery<>), new[] { typeof(ISeparatedQuery<>).GetTypeInfo().Assembly });
            _ioc.Register(typeof(ISeparatedQueryHandler<,>), new[] { typeof(ISeparatedQueryHandler<,>).GetTypeInfo().Assembly });

            _ioc.RegisterCollection(typeof(ISeparatedCommand), new[] { typeof(ISeparatedCommand).GetTypeInfo().Assembly });
            _ioc.Register(typeof(ISeparatedCommandHandler<>), new[] { typeof(ISeparatedCommandHandler<>).GetTypeInfo().Assembly });
        }
    }
}