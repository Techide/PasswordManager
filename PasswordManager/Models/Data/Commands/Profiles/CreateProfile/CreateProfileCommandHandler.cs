﻿using PasswordManager.Models.Data.EF;
using PasswordManager.Services.Cryptography;
using PasswordManager.Services.Settings;

namespace PasswordManager.Models.Data.Commands {

    public class CreateProfileCommandHandler : ISeparatedCommandHandler<CreateProfileCommand> {

        public void Execute(CreateProfileCommand command) {
            using (var db = new PasswordManagerContext()) {
                var pwd = Cryptographer.Encrypt(command.Password, AppSettings.MasterPassword);
                db.Profiles.Add(new Profile {
                    Name = command.Profile,
                    Password = pwd.EncryptedPassword,
                    Account = command.Account,
                    IV = pwd.IV,
                    Salt = pwd.Salt
                });
                db.SaveChanges();
            }
        }
    }
}