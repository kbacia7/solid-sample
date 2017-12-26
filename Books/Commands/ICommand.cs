using System.Collections.Generic;


public interface ICommand
{
    string Name { get; }
    void Execute(List<string> args, BookContext BookContext);
}

