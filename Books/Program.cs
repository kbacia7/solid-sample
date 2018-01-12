using System;

namespace Books
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Commands: ");
            Console.WriteLine("/add <book/author>");
            Console.WriteLine("/view <book/author>");
            Console.WriteLine("/find <book/author> <ID>");
            Console.WriteLine("/remove <book/author> <ID>");
            Console.WriteLine("/stop");

            BookContext bC = new BookContext();
            ConsoleErrorOutput errorOutput = new ConsoleErrorOutput();
            TableNameValidator tableNameValidator = new TableNameValidator();
            CorrectFloatDataValidator correctFloatData = new CorrectFloatDataValidator();
            CorrectIntDataValidator correctIntData = new CorrectIntDataValidator();
           
            Book book = new Book();
            Author author = new Author();
            InputReader inputReader = new InputReader();

            IValidator[] validatorsForManagers = new IValidator[]
            {
                correctIntData, correctFloatData
            };
            BookManager bookManager = new BookManager(validatorsForManagers, book, inputReader, bC);
            AuthorManager authorManager = new AuthorManager(validatorsForManagers[0], author, inputReader, bC);

            BookExistsValidator bookExistsValidator = new BookExistsValidator(bookManager);
            AuthorExistsValidator authorExistsValidator = new AuthorExistsValidator(authorManager);

            IValidator[] validators = new IValidator[]
            {
               tableNameValidator, correctFloatData, correctIntData, bookExistsValidator, authorExistsValidator
            };
            
            IManager[] managers = new IManager[]
            {
                authorManager, bookManager
            };

            CommandRegistration commandRegistration = new CommandRegistration();
            CommandManager commandManager = new CommandManager(validators, errorOutput, managers, commandRegistration);
            LengthValidator lengthValidator = new LengthValidator(commandManager);
            CommandExistsValidator commandExistsValidator = new CommandExistsValidator(commandManager);

            CommandSplit commandSplit = new CommandSplit(errorOutput);
            CommandExecutor command = new CommandExecutor(lengthValidator, errorOutput, bC);
            LineInterpreter lineInterpreter = new LineInterpreter(commandExistsValidator, errorOutput, commandSplit, commandManager, command);

            while (true)
            {
                string line = inputReader.ReadInput();
                lineInterpreter.Interpret(line);
            }
        }
    }
}