namespace PasswordManager.Models.Data.Commands {

    public interface ISeparatedCommandHandler<TCommand> where TCommand : ISeparatedCommand {

        void Execute(TCommand command);
    }
}