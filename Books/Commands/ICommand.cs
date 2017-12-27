using System.Collections.Generic;


public interface ICommand
{
    void Execute(List<string> args, BookContext BookContext);
    void SetErrorOutput(IErrorOutput errorOutput);
}