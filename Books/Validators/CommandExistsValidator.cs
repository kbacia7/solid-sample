using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CommandExistsValidator : IValidator
{
    public ValidatorResult Validate(string commandName)
    {
        CommandListener commandListener = new CommandListener();
        if (commandListener.IsCommandExists(commandName))
            return new ValidatorResult(ErrorCode.NO_ERROR);
        return new ValidatorResult(ErrorCode.COMMAND_NOT_EXISTS);
    }
}

