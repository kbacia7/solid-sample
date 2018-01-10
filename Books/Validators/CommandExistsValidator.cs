using Ninject;

public class CommandExistsValidator : IValidator
{
    [Inject]
    public CommandManager CommandManager { get; set; }

    public ValidatorResult Validate(string commandName)
    {
        if (CommandManager.IsCommandExists(commandName))
            return new ValidatorResult(ErrorCode.NO_ERROR);
        return new ValidatorResult(ErrorCode.COMMAND_NOT_EXISTS);
    }
}