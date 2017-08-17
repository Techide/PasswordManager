namespace PasswordManager.Models.Data.Commands {

    public class DeleteProfileCommand : ISeparatedCommand {
        public int Id { get; set; }
    }
}