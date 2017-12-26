using System.Collections.Generic;


public class LengthValidator : IValidator
{
    public ValidatorResult Validate(string data)
    {
        CommandListener commandListener = new CommandListener();
        string[] args = data.Split(' ');
        string commandName = args[0];
        int argsC = commandListener.GetCommandArgsCountByName(commandName);
        int argLen = args.Length;
        if (argsC != argLen - 1)
            return new ValidatorResult(ErrorCode.INVALID_LEN_ARGS);
        return new ValidatorResult(ErrorCode.NO_ERROR);
    }
}

