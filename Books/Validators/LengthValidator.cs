public class LengthValidator : IValidator
{
    private CommandListener commandListener;
    public LengthValidator(CommandListener _commandListener)
    {
        commandListener = _commandListener;
    }
    public ValidatorResult Validate(string data)
    {
        string[] args = data.Split(' ');
        string commandName = args[0];
        int argsC = commandListener.GetCommandArgsCountByName(commandName);
        int argLen = args.Length;
        if (argsC != argLen - 1)
            return new ValidatorResult(ErrorCode.INVALID_LEN_ARGS);
        return new ValidatorResult(ErrorCode.NO_ERROR);
    }
}