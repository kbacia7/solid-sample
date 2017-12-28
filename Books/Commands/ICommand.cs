using System.Collections.Generic;

public interface ICommand
{
    void Execute(IList<string> args, BookContext BookContext);
}