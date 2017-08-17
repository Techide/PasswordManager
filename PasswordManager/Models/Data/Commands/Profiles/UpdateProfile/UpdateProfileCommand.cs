namespace PasswordManager.Models.Data.Commands {

    public class UpdateProfileCommand : ISeparatedCommand {
        public int Id { get; set; }
        public string Profile { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
    }
}