using System.Collections.Generic;
using System.Linq;

public class CommandSplit
{
    private IErrorOutput errorOutput;

    public CommandSplit(IErrorOutput _errorOutput)
    {
        errorOutput = _errorOutput;
    }

    public IList<string> Split(string command)
    {
        string[] args = command.Split(' ');
        args[0] = args[0].Remove(0, 1);
        return args.ToList();
    }
}