using System;
using System.Diagnostics;
using Windows.Storage;

namespace PasswordManager.Util {
    internal static class SettingsProvider {

        private static string _password;

#if DEBUG
        internal static string Password {
            get {
                GetPassword();
                return _password;
            }
            private set {
                _password = value;
            }
        }

        private static async void GetPassword() {
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
        }
#else
        internal static string Password {
            get {
                return _password;
            }
            set {
                _password = value;
            }
        }
#endif


    }
}
