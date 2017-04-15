using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Models.Data.Commands.MasterPassword {

    public class CreateMasterPasswordCommand : ISeparatedCommand {
        public string Password { get; set; }
    }
}