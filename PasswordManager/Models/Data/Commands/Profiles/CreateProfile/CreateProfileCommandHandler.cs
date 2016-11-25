using PasswordManager.Data.EF;
using PasswordManager.Data.EF.Entities;
using PasswordManager.Services.Cryptography;

namespace PasswordManager.Models.Data.Commands.Profiles.CreateProfile {

    public class CreateProfileCommandHandler : ISeparatedCommandHandler<CreateProfileCommand> {

        public void Execute(CreateProfileCommand command) {
            using (var db = new PasswordManagerContext()) {
                var pwd = Cryptographer.Encrypt(command.Password);
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