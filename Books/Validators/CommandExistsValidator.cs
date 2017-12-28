public class CommandExistsValidator : IValidator
{
    private CommandListener commandListener;
    public CommandExistsValidator(CommandListener _commandListener)
    {
        commandListener = _commandListener;
    }
    public ValidatorResult Validate(string commandName)
    {
        if (commandListener.IsCommandExists(commandName))
            return new ValidatorResult(ErrorCode.NO_ERROR);
        return new ValidatorResult(ErrorCode.COMMAND_NOT_EXISTS);
    }
}