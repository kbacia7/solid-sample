using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class CommandListener
{
    Dictionary<string, CommandParams> CommandList = new Dictionary<string, CommandParams>()
    {
        {
            "add",
            new CommandParams()
            {
                RequiredArgs = new Dictionary<string, Type>()
                {
                    {"type", typeof(string)}
                },
                Command = new CommandAdd(new IValidator[] {
                    new TypeValidator(), new DataTypeValidator()
                })
            }
        }
    };


    public ICommand GetCommandByName(string name, IErrorOutput _errorOutput)
    {
        ICommand command = CommandList[name].Command;
        command.SetErrorOutput(_errorOutput);
        return command;
    }

    public int GetCommandArgsCountByName(string name)
    {
        return CommandList[name].RequiredArgs.Count;
    }

    public bool IsCommandExists(string name)
    {
        if (CommandList.ContainsKey(name))
            return true;
        return false;
    }
}