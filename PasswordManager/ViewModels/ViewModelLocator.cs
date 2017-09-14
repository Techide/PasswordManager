using PasswordManager.Models.Data.Commands;
using PasswordManager.Models.Data.EF;
using PasswordManager.Models.Data.Queries;
using PasswordManager.Services.Frames;
using PasswordManager.Services.Navigation;
using SimpleInjector;
using System.Reflection;
using Windows.UI.Xaml.Controls;

namespace PasswordManager.ViewModels {

    public class ViewModelLocator {
        private static Container _ioc;

        static ViewModelLocator() {
            _ioc = new Container();
            Bootstrap();
        }

        public static MainViewModel Main { get { return _ioc.GetInstance<MainViewModel>(); } }

        public static CreateProfileViewModel CreateProfile { get { return _ioc.GetInstance<CreateProfileViewModel>(); } }

        public static EditProfileViewModel EditProfile { get { return _ioc.GetInstance<EditProfileViewModel>(); } }

        public static MasterPasswordQueryViewModel MasterPasswordQuery { get { return _ioc.GetInstance<MasterPasswordQueryViewModel>(); } }

        public static MasterPasswordCreateViewModel MasterPasswordCreate { get { return _ioc.GetInstance<MasterPasswordCreateViewModel>(); } }

        public static NavigationMenuViewModel NavigationMenu { get { return _ioc.GetInstance<NavigationMenuViewModel>(); } }

        public static SettingsViewModel Settings { get { return _ioc.GetInstance<SettingsViewModel>(); } }

        public static FrameService FrameService { get { return _ioc.GetInstance<FrameService>(); } }

        private static void Bootstrap() {
            _ioc.Register<PasswordManagerContext>(Lifestyle.Transient);
            _ioc.Register<FrameService>(Lifestyle.Singleton);
            _ioc.Register<INavigationService, NavigationService>(Lifestyle.Singleton);

            _ioc.RegisterCollection(typeof(IViewModel), new[] { typeof(IViewModel).GetTypeInfo().Assembly });
            _ioc.Register(typeof(ISeparatedQuery<>), new[] { typeof(ISeparatedQuery<>).GetTypeInfo().Assembly });
            _ioc.Register(typeof(ISeparatedQueryHandler<,>), new[] { typeof(ISeparatedQueryHandler<,>).GetTypeInfo().Assembly });

            _ioc.RegisterCollection(typeof(ISeparatedCommand), new[] { typeof(ISeparatedCommand).GetTypeInfo().Assembly });
            _ioc.Register(typeof(ISeparatedCommandHandler<>), new[] { typeof(ISeparatedCommandHandler<>).GetTypeInfo().Assembly });
        }
    }
}