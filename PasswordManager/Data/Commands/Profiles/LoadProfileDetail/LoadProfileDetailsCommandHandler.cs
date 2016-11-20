namespace PasswordManager.Data.Commands.Profiles.LoadProfileDetail {
    public class LoadProfileDetailsCommandHandler : ISeparatedCommandHandler<LoadProfileDetailsCommand> {
        public void Execute(LoadProfileDetailsCommand command) {
            command.ViewModel.LoadDetails(command.AccountId);
        }
    }
}
