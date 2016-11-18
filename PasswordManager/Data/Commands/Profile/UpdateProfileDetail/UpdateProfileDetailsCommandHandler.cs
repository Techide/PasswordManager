namespace PasswordManager.Data.Commands.Profile.UpdateProfileDetail {
    public class UpdateProfileDetailsCommandHandler : ISeparatedCommandHandler<UpdateProfileDetailsCommand> {
        public void Execute(UpdateProfileDetailsCommand command) {
            command.ViewModel.LoadDetails(command.AccountId);
        }
    }
}
