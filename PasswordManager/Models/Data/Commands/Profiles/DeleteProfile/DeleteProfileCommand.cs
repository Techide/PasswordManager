using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Models.Data.Commands.Profiles.DeleteProfile {

    public class DeleteProfileCommand : ISeparatedCommand {
        public int Id { get; set; }
    }
}