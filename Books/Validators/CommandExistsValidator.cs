public class CommandExistsValidator : IValidator
{
    private CommandManager commandManager;

    public CommandExistsValidator(CommandManager _commandManager)
    {
        commandManager = _commandManager;
    }

    public ValidatorResult Validate(string commandName)
    {
        if (commandManager.IsCommandExists(commandName))
            return new ValidatorResult(ErrorCode.NO_ERROR);
        return new ValidatorResult(ErrorCode.COMMAND_NOT_EXISTS);
    }
}