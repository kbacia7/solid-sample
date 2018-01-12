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

            BookContext bookContext = new BookContext();

            //Validators
            TableNameValidator tableNameValidator = new TableNameValidator();
            CorrectFloatDataValidator correctFloatData = new CorrectFloatDataValidator();
            CorrectIntDataValidator correctIntData = new CorrectIntDataValidator();

            //Default Errour Output, empty Book & Author objects for managers and Input Reader
            ConsoleErrorOutput consoleErrorOutput = new ConsoleErrorOutput();
            Book book = new Book();
            Author author = new Author();
            InputReader inputReader = new InputReader();

            //Array with validators for managers
            IValidator[] validatorsForManagers = new IValidator[]
            {
                correctIntData, correctFloatData
            };

            //Managers
            BookManager bookManager = new BookManager(validatorsForManagers, book, inputReader, bookContext);
            AuthorManager authorManager = new AuthorManager(validatorsForManagers[0], author, inputReader, bookContext);

            //Validators that require managers
            BookExistsValidator bookExistsValidator = new BookExistsValidator(bookManager);
            AuthorExistsValidator authorExistsValidator = new AuthorExistsValidator(authorManager);

            //Array with validators for command manager
            IValidator[] validators = new IValidator[]
            {
               tableNameValidator, correctFloatData, correctIntData, bookExistsValidator, authorExistsValidator
            };
            
            //And array with managers for command manager
            IManager[] managers = new IManager[]
            {
                authorManager, bookManager
            };

           
            CommandRegistration commandRegistration = new CommandRegistration();

            //Command manager & validators that require command manager
            CommandManager commandManager = new CommandManager(validators, consoleErrorOutput, managers, commandRegistration);
            LengthValidator lengthValidator = new LengthValidator(commandManager);
            CommandExistsValidator commandExistsValidator = new CommandExistsValidator(commandManager);
            
            //Command Split, executor commands and interpreter
            CommandSplit commandSplit = new CommandSplit(consoleErrorOutput);
            CommandExecutor commandExecutor = new CommandExecutor(lengthValidator, consoleErrorOutput, bookContext);
            LineInterpreter lineInterpreter = new LineInterpreter(commandExistsValidator, consoleErrorOutput, commandSplit, commandManager, commandExecutor);

            while (true)
            {
                string line = inputReader.ReadInput();
                lineInterpreter.Interpret(line);
            }
        }
    }
}