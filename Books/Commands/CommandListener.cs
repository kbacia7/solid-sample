using System;
using System.Collections.Generic;
using System.Linq;

public class CommandListener
{
    Dictionary<string, CommandParams> commandList = new Dictionary<string, CommandParams>()
    {
         {
            "add",
            new CommandParams()
            { 
                RequiredArgs = new Dictionary<string, Type>()
                {
                    { "type", typeof(string)}
                }
            }
        }
    };
    public Dictionary<string, CommandParams> CommandList
    {
        get { return commandList; }
    }

    public CommandListener(IValidator[] validators, IErrorOutput errorOutput)
    {
        DataTypeValidator dataTypeValidator = (DataTypeValidator)(from validator in validators where validator.GetType() == typeof(DataTypeValidator) select validator).FirstOrDefault();
        TypeValidator typeValidator = (TypeValidator)(from validator in validators where validator.GetType() == typeof(TypeValidator) select validator).FirstOrDefault();
        commandList["add"].Command = new CommandAdd(new IValidator[] {
            typeValidator,
            dataTypeValidator
        }, errorOutput);
    }

    public ICommand GetCommandByName(string name)
    {
        ICommand command = commandList[name].Command;
        return command;
    }

    public int GetCommandArgsCountByName(string name)
    {
        return commandList[name].RequiredArgs.Count;
    }

    public bool IsCommandExists(string name)
    {
        if (commandList.ContainsKey(name))
            return true;
        return false;
    }
}