public class LengthValidator : IValidator
{
    private CommandManager commandManager;
    public LengthValidator(CommandManager _commandManager)
    {
        commandManager = _commandManager;
    }
    public ValidatorResult Validate(string data)
    {
        string[] args = data.Split(' ');
        string commandName = args[0];
        int argsC = commandManager.GetCommandArgsCountByName(commandName);
        int argLen = args.Length;
        if (argsC != argLen - 1)
            return new ValidatorResult(ErrorCode.INVALID_LEN_ARGS);
        return new ValidatorResult(ErrorCode.NO_ERROR);
    }
}