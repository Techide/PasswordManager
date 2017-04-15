using PasswordManager.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Models.Data.Commands.Profiles.DeleteProfile {

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