namespace PasswordManager.Models.Data.Commands {

    public class LoadProfileDetailsCommandHandler : ISeparatedCommandHandler<LoadProfileDetailsCommand> {

        public void Execute(LoadProfileDetailsCommand command) {
            command.ViewModel.LoadDetails(command.AccountId);
        }
    }
}