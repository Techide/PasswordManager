using PasswordManager.Data.EF;
using PasswordManager.Data.EF.Entities;
using PasswordManager.Models.Data.EF.Entities;
using PasswordManager.Services.Cryptography;
using PasswordManager.Services.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Models.Data.Commands.MasterPassword {

    public class CreateMasterPasswordCommandHandler : ISeparatedCommandHandler<CreateMasterPasswordCommand> {

        public void Execute(CreateMasterPasswordCommand command) {
            using (var db = new PasswordManagerContext()) {
                var pwd = Cryptographer.Encrypt(command.Password);
                var profile = new Profile {
                    Name = "Master",
                    Password = pwd.EncryptedPassword,
                    Account = "Master",
                    IV = pwd.IV,
                    Salt = pwd.Salt
                };

                db.Profiles.Add(profile);
                db.SaveChanges();

                db.Settings.Add(new Setting { Key = SettingsService.Keys.MASTER_PASSWORD, Value = profile.Id.ToString() });
                db.SaveChanges();
            }
        }
    }
}