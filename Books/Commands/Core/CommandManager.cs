using System;
using System.Collections.Generic;
using System.Linq;

public class CommandManager
{
    private CommandRegistration commandRegistration;

    public CommandManager(IValidator[] validators, IErrorOutput errorOutput, CommandRegistration _commandRegistration)
    {
        commandRegistration = _commandRegistration;

        //Validators for commands
        DataTypeValidator dataTypeValidator = (DataTypeValidator)(from validator in validators where validator.GetType() == typeof(DataTypeValidator) select validator).FirstOrDefault();
        TypeValidator typeValidator = (TypeValidator)(from validator in validators where validator.GetType() == typeof(TypeValidator) select validator).FirstOrDefault();

        CommandAdd commandAdd = new CommandAdd(new IValidator[] {
            typeValidator,
            dataTypeValidator
        }, errorOutput);
        CommandStop commandStop = new CommandStop(errorOutput);

        commandRegistration.RegisterCommand("add", commandAdd);
        commandRegistration.RegisterCommand("stop", commandStop);
    }

    public ICommand GetCommandByName(string name)
    {
        ICommand command = commandRegistration.CommandList[name].Command;
        return command;
    }

    public int GetCommandArgsCountByName(string name)
    {
        return commandRegistration.CommandList[name].RequiredArgs.Count;
    }

    public bool IsCommandExists(string name)
    {
        if (commandRegistration.CommandList.ContainsKey(name))
            return true;
        return false;
    }
}