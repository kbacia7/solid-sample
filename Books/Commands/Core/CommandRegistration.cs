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
            "view",
            new CommandParams()
            {
                RequiredArgs = new Dictionary<string, Type>()
                {
                    { "type", typeof(string)}
                }
            }
        },
        {
            "find",
            new CommandParams()
            {
                RequiredArgs = new Dictionary<string, Type>()
                {
                    {"type", typeof(string) },
                    {"ID", typeof(int) }
                }
            }
        },
        {
            "remove",
            new CommandParams()
            {
                RequiredArgs = new Dictionary<string, Type>()
                {
                    {"type", typeof(string) },
                    {"ID", typeof(int) }
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
