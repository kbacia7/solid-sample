using System;
using System.Collections.Generic;

namespace Books
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleErrorOutput errorOutput = new ConsoleErrorOutput();
            TypeValidator typeValidator = new TypeValidator();
            DataTypeValidator dataTypeValidator = new DataTypeValidator();
            CommandFormatValidator commandFormatValidator = new CommandFormatValidator();
            IValidator[] validators = new IValidator[]
            {
                dataTypeValidator, typeValidator
            };
            CommandListener commandListener = new CommandListener(validators, errorOutput);
            LengthValidator lengthValidator = new LengthValidator(commandListener);
            CommandExistsValidator commandExistsValidator = new CommandExistsValidator(commandListener);

            Console.WriteLine("Commands: ");
            Console.WriteLine("/add <book/author>");
            BookContext bC = new BookContext();
            ParseCommand parser = new ParseCommand(commandFormatValidator, errorOutput);
            CommandExecutor command = new CommandExecutor(lengthValidator, errorOutput, bC);
            ICommand commandToExecute = null;
            InputReader inputReader = new InputReader();
            
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
                            commandToExecute = commandListener.GetCommandByName(commandName);
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