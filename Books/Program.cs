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
            Console.WriteLine("/stop");

            ConsoleErrorOutput errorOutput = new ConsoleErrorOutput();
            TypeValidator typeValidator = new TypeValidator();
            DataTypeValidator dataTypeValidator = new DataTypeValidator();
            IValidator[] validators = new IValidator[]
            {
                dataTypeValidator, typeValidator
            };
            CommandRegistration commandRegistration = new CommandRegistration();
            CommandManager commandManager = new CommandManager(validators, errorOutput, commandRegistration);
            LengthValidator lengthValidator = new LengthValidator(commandManager);
            CommandExistsValidator commandExistsValidator = new CommandExistsValidator(commandManager);
            BookContext bC = new BookContext();
            CommandSplit commandSplit = new CommandSplit(errorOutput);
            CommandExecutor command = new CommandExecutor(lengthValidator, errorOutput, bC);
            InputReader inputReader = new InputReader();
            LineInterpreter lineInterpreter = new LineInterpreter(commandExistsValidator, errorOutput, commandSplit, commandManager, command);
            while (true)
            {
                string line = inputReader.ReadInput();
                lineInterpreter.Interpret(line);
            }
        }
    }
}