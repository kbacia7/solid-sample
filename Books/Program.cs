using System;
using System.Collections.Generic;

namespace Books
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Commands: ");
            Console.WriteLine("/add <book/author>");
            BookContext bC = new BookContext();
            ParseCommand parser = new ParseCommand();
            CommandExecutor command = new CommandExecutor(bC);
            ICommand commandToExecute = null;
            InputReader inputReader = new InputReader();
            CommandListener commandListener = new CommandListener();
            CommandExistsValidator commandExistsValidator = new CommandExistsValidator();
            ValidatorResult validatorResult = null;
            ErrorOutput errorOutput = new ErrorOutput();
            while(true)
            {
                string line = inputReader.ReadInput();
                if (line.Length > 0)
                {
                    List<string> data = parser.Parse(line);
                    if (data != null)
                    {
                        string commandName = data[0];
                        validatorResult = commandExistsValidator.Validate(commandName);
                        if (validatorResult.Success)
                        {
                            commandToExecute = commandListener.GetCommandByName(commandName);
                            command.ExecuteCommand(commandToExecute, data);
                        }
                        else
                            errorOutput.ErrorParse(validatorResult.ErrorCode);
                    }
                }
                else
                    break;
            }
        }
    }
}