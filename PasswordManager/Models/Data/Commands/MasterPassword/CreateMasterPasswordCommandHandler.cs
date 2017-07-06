using PasswordManager.Data.EF;
using PasswordManager.Services.Cryptography;
using PasswordManager.Services.Settings;
using System.Linq;

namespace PasswordManager.Models.Data.Commands.MasterPassword {

    public class CreateMasterPasswordCommandHandler : ISeparatedCommandHandler<CreateMasterPasswordCommand> {

        public void Execute(CreateMasterPasswordCommand command) {
            using (var db = new PasswordManagerContext()) {
                var mp = db.Settings.Single(x => x.Name == AppSettings.MASTER_PASSWORD_KEY);
                mp.Value = SecurePasswordHasher.Hash(command.Password);
                db.Update(mp);
                db.SaveChanges();
                AppSettings.MasterPassword = command.Password;
            }
        }
    }
}