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

            ICommand commandToExecute = null;
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
            ValidatorResult validatorResult = null;

            while (true)
            {
                string line = inputReader.ReadInput();
                List<string> data = (List<string>)commandSplit.Split(line);
                if (data != null && data.Count > 0)
                {
                    string commandName = data[0];
                    validatorResult = commandExistsValidator.Validate(commandName);
                    if (validatorResult.Success)
                    {
                        commandToExecute = commandManager.GetCommandByName(commandName);
                        command.ExecuteCommand(commandToExecute, data);
                    }
                    else
                        errorOutput.WriteError(validatorResult.ErrorCode);
                }
            }
        }
    }
}