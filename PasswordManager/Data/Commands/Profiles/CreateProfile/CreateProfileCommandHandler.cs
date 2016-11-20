using PasswordManager.Data.EF;

namespace PasswordManager.Data.Commands.Profiles.CreateProfile {
    public class CreateProfileCommandHandler : ISeparatedCommandHandler<CreateProfileCommand> {
        public void Execute(CreateProfileCommand command) {
            using (var db = new PasswordManagerContext()) {
                db.Profiles.Add(command.Profile);
            }
        }
    }
}
