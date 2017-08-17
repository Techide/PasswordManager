using PasswordManager.Models.Data.EF;

namespace PasswordManager.Models.Data.Commands {

    public class DeleteProfileCommandHandler : ISeparatedCommandHandler<DeleteProfileCommand> {

        public void Execute(DeleteProfileCommand command) {
            using (var db = new PasswordManagerContext()) {
                var profile = db.Profiles.Find(command.Id);
                db.Remove(profile);
                db.SaveChanges();
            }
        }
    }
}