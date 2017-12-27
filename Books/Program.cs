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
            LengthValidator lengthValidator = new LengthValidator();
            CommandFormatValidator commandFormatValidator = new CommandFormatValidator();
            ConsoleErrorOutput errorOutput = new ConsoleErrorOutput();
            ParseCommand parser = new ParseCommand(commandFormatValidator, errorOutput);
            CommandExecutor command = new CommandExecutor(lengthValidator, errorOutput, bC);
            ICommand commandToExecute = null;
            InputReader inputReader = new InputReader();
            CommandListener commandListener = new CommandListener();
            CommandExistsValidator commandExistsValidator = new CommandExistsValidator();
            ValidatorResult validatorResult = null;
          
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
                            commandToExecute = commandListener.GetCommandByName(commandName, errorOutput);
                            command.ExecuteCommand(commandToExecute, data);
                        }
                        else
                            errorOutput.WriteError(validatorResult.ErrorCode);
                    }
                }
                else
                    break;
            }
        }
    }
}