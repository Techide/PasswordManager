using PasswordManager.Data.EF;
using System.Linq;

namespace PasswordManager.Services.Settings {

    internal static class AppSettings {
        public const string MASTER_PASSWORD_KEY = "MasterPassword";

        public static string MasterPassword { get; set; }

        public static bool HasMasterPassword { get { return GetHasMasterPassword(); } }

        private static bool GetHasMasterPassword() {
            var context = new PasswordManagerContext();
            var pwd = context.Settings.Single(x => x.Name == MASTER_PASSWORD_KEY);
            return pwd.Value != null ? pwd.Value.Any() : false;
        }
    }
}