using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Models.Data.Commands.Profiles.UpdateProfile {

    public class UpdateProfileCommand : ISeparatedCommand {
        public int Id { get; set; }
        public string Profile { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
    }
}