﻿using PasswordManager.Data.EF;
using PasswordManager.Services.Cryptography;
using System;

namespace PasswordManager.Models.Data.Commands.Profiles.UpdateProfile {

    public class UpdateProfileCommandHandler : ISeparatedCommandHandler<UpdateProfileCommand> {

        public void Execute(UpdateProfileCommand command) {
            using (var db = new PasswordManagerContext()) {
                var pwd = Cryptographer.Encrypt(command.Password);
                var profile = db.Profiles.Find(command.Id);
                profile.Name = command.Profile;
                profile.Account = command.Account;
                profile.Password = pwd.EncryptedPassword;
                profile.IV = pwd.IV;
                profile.Salt = pwd.Salt;
                db.SaveChanges();
            }
        }
    }
}