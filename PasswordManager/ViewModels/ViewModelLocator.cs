using PasswordManager.Data.EF;
using PasswordManager.Data.Queries;
using PasswordManager.Models.Data.Commands;
using SimpleInjector;
using System.Reflection;

namespace PasswordManager.ViewModels {

    public class ViewModelLocator {
        private static Container _ioc;

        static ViewModelLocator() {
            _ioc = new Container();
            Bootstrap();
        }

        public static MainPageViewModel MainPage { get { return _ioc.GetInstance<MainPageViewModel>(); } }

        public static CreateProfileViewModel CreateProfilePage { get { return _ioc.GetInstance<CreateProfileViewModel>(); } }

        public static EditProfileViewModel EditProfilePage { get { return _ioc.GetInstance<EditProfileViewModel>(); } }

        public static MasterPasswordQueryViewModel MasterPasswordQueryPage { get { return _ioc.GetInstance<MasterPasswordQueryViewModel>(); } }

        public static MasterPasswordCreateViewModel MasterPasswordCreatePage { get { return _ioc.GetInstance<MasterPasswordCreateViewModel>(); } }

        private static void Bootstrap() {
            _ioc.Register<PasswordManagerContext>(Lifestyle.Transient);

            _ioc.RegisterCollection(typeof(IViewModel), new[] { typeof(IViewModel).GetTypeInfo().Assembly });
            _ioc.Register(typeof(ISeparatedQuery<>), new[] { typeof(ISeparatedQuery<>).GetTypeInfo().Assembly });
            _ioc.Register(typeof(ISeparatedQueryHandler<,>), new[] { typeof(ISeparatedQueryHandler<,>).GetTypeInfo().Assembly });

            _ioc.RegisterCollection(typeof(ISeparatedCommand), new[] { typeof(ISeparatedCommand).GetTypeInfo().Assembly });
            _ioc.Register(typeof(ISeparatedCommandHandler<>), new[] { typeof(ISeparatedCommandHandler<>).GetTypeInfo().Assembly });
        }
    }
}