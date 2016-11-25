namespace PasswordManager.Models.Data.Commands.Profiles.CreateProfile {

    public class CreateProfileCommand : ISeparatedCommand {
        public string Profile { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
    }
}