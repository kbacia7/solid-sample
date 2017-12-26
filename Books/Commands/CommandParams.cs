using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class CommandParams
{
    public Dictionary<string, Type> RequiredArgs;
    public ICommand Command;
}