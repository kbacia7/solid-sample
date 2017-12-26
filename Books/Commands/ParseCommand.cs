using System.Collections.Generic;
using System.Linq;


public class ParseCommand
{
    public List<string> Parse(string command)
    {       
        CommandFormatValidator validator = new CommandFormatValidator();
        ValidatorResult validatorResult = validator.Validate(command);
        ErrorOutput errorOutput = new ErrorOutput();
        if (validatorResult.Success)
        {
            string[] args = command.Split(' ');
            args[0] = args[0].Remove(0, 1);
            return args.ToList();
        }
        else
            errorOutput.ErrorParse(validatorResult.ErrorCode);
        return null;
    }
}