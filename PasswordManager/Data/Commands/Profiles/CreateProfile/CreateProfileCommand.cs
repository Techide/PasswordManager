using PasswordManager.Data.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Data.Commands.Profiles.CreateProfile {
    public class CreateProfileCommand : ISeparatedCommand {
        public Profile Profile { get; set; }
    }
}
