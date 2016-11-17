using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Storage;

namespace PasswordManager.Util {
    internal static class SettingsProvider {

        private static string _password;

        static SettingsProvider() {
            _password = GetPassword().Result;
        }

        internal static string Password {
            get {
                return _password;
            }
            private set {
                _password = value;
            }
        }

#if DEBUG
        private static async Task<string> GetPassword() {
            var result = string.Empty;
            var localFolder = ApplicationData.Current.LocalFolder;
            var name = "dev.txt";
            try {
                var op = await localFolder.CreateFileAsync(name, CreationCollisionOption.OpenIfExists);
                _password = await FileIO.ReadTextAsync(op);
            }
            catch (Exception ex) {
                Debug.WriteLine(ex);
            }
            return _password;
        }
#endif

    }
}
