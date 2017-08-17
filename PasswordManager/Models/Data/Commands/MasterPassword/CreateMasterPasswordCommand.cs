namespace PasswordManager.Models.Data.Commands {

    public class CreateMasterPasswordCommand : ISeparatedCommand {
        public string Password { get; set; }
    }
}