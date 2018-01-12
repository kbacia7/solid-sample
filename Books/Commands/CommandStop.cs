using System;
using System.Collections.Generic;

public class CommandStop : ICommand
{
    public void Execute(IList<string> args, BookContext BookContext)
    {
        Environment.Exit(0);
    }
}