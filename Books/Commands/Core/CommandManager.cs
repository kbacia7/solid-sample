using System;
using System.Collections.Generic;
using System.Linq;

public class CommandManager
{
    public CommandRegistration CommandRegistration { get; set; }
    public IErrorOutput ErrorOutput { get; set; }
    public IValidator[] Validators { get; set; }


    public void RegisterCommands()
    {
        DataTypeValidator dataTypeValidator = (DataTypeValidator)(from validator in Validators where validator.GetType() == typeof(DataTypeValidator) select validator).FirstOrDefault();
        TypeValidator typeValidator = (TypeValidator)(from validator in Validators where validator.GetType() == typeof(TypeValidator) select validator).FirstOrDefault();

        CommandAdd commandAdd = new CommandAdd(new IValidator[] {
            typeValidator,
            dataTypeValidator
        }, ErrorOutput);
        CommandStop commandStop = new CommandStop(ErrorOutput);

        CommandRegistration.RegisterCommand("add", commandAdd);
        CommandRegistration.RegisterCommand("stop", commandStop);
    }

    public ICommand GetCommandByName(string name)
    {
        ICommand command = CommandRegistration.CommandList[name].Command;
        return command;
    }

    public int GetCommandArgsCountByName(string name)
    {
        return CommandRegistration.CommandList[name].RequiredArgs.Count;
    }

    public bool IsCommandExists(string name)
    {
        if (CommandRegistration.CommandList.ContainsKey(name))
            return true;
        return false;
    }
}