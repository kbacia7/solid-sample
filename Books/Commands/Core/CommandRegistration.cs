using System;
using System.Collections.Generic;

public class CommandRegistration
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
        },
        {
            "stop",
            new CommandParams()
            {
                RequiredArgs = new Dictionary<string, Type>()
            }
        }
    };

    public Dictionary<string, CommandParams> CommandList
    {
        get { return commandList; }
    }

    public void RegisterCommand(string name, ICommand command)
    {
        commandList[name].Command = command;
    }
}
