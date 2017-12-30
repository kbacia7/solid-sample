using System;
using System.Collections.Generic;

public class CommandStop : ICommand
{
    private IErrorOutput errorOutput;

    public CommandStop(IErrorOutput _errorOutput)
    {
        errorOutput = _errorOutput;
    }

    public void Execute(IList<string> args, BookContext BookContext)
    {
        Environment.Exit(0);
    }
}