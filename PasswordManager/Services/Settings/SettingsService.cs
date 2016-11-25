using System;
using System.Threading.Tasks;
using Windows.Storage;

namespace PasswordManager.Services.Settings {

    internal static class SettingsService {
        private static string _password;

        internal static string Password {
            get {
                return _password;
            }
            set {
                _password = value;
            }
        }

#if DEBUG

        public static async void Initialize() {
            var localFolder = ApplicationData.Current.LocalFolder;
            var name = "dev.txt";
            var op = await localFolder.CreateFileAsync(name, CreationCollisionOption.OpenIfExists);
            _password = await FileIO.ReadTextAsync(op);
        }

#endif
    }
}