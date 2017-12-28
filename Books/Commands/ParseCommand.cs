using System.Collections.Generic;
using System.Linq;

public class ParseCommand
{
    IValidator validator;
    ConsoleErrorOutput errorOutput;
    public ParseCommand(IValidator _validator, ConsoleErrorOutput _errorOutput)
    {
        validator = _validator;
        errorOutput = _errorOutput;
    }

    public List<string> Parse(string command)
    {       
        ValidatorResult validatorResult = validator.Validate(command);
        if (validatorResult.Success)
        {
            string[] args = command.Split(' ');
            args[0] = args[0].Remove(0, 1);
            return args.ToList();
        }
        else
            errorOutput.WriteError(validatorResult.ErrorCode);
        return null;
    }
}