using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class CommandStop : ICommand
{
    IErrorOutput errorOutput;
    public CommandStop(IErrorOutput _errorOutput)
    {
        errorOutput = _errorOutput;
    }

    public void Execute(IList<string> args, BookContext BookContext)
    {
        Environment.Exit(0);
    }
}

