using System;
using System.Collections.Generic;

public class CommandParams
{
    public Dictionary<string, Type> RequiredArgs;
    public ICommand Command;
}