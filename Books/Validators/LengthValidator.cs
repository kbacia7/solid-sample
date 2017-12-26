using System.Collections.Generic;


public class LengthValidator : IValidator
{
    Dictionary<string, int> ArgsLength = new Dictionary<string, int>()
    {
        {"add", 1},
        {"stop", 0}
    };
    public ValidatorResult Validate(string data)
    {
        string[] args = data.Split(' ');
        string commandName = args[0];
        int argLen = args.Length;
        if (ArgsLength[commandName] != argLen - 1)
            return new ValidatorResult(ErrorCode.INVALID_LEN_ARGS);
        return new ValidatorResult(ErrorCode.NO_ERROR);
    }
}

