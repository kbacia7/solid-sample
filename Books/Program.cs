using System;
using System.Collections.Generic;

namespace Books
{
    class Program
    {
        static void Main(string[] args)
        {
            BookContext bC = new BookContext();
            Console.WriteLine("Commands: ");
            Console.WriteLine("/add <book/author>");
            ParseCommand parser = new ParseCommand();
            Command command = new Command(bC);
            ICommand commandToExecute = null;
            while(true)
            {
                string line = Console.ReadLine();
                if (line.Length > 0)
                {
                    List<string> data = parser.Parse(line);
                    if (data != null)
                    {
                        string commandName = data[0];
                        switch (commandName)
                        {
                            case "add":
                                {
                                    commandToExecute = new CommandAdd();
                                    break;
                                }
                        }
                        command.ExecuteCommand(commandToExecute, data);
                    }
                }
                else
                    break;
            }
        }
    }
}
