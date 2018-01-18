using System.Linq;

public class CommandManager
{
    public CommandRegistration CommandRegistration { get; set; }
    public IErrorOutput ErrorOutput { get; set; }
    public IValidator[] Validators { get; set; }
    public IManager[] Managers { get; set; }


    public void RegisterCommands()
    {
        TableNameValidator tableNameValidator = (TableNameValidator)(from validator in Validators where validator.GetType() == typeof(TableNameValidator) select validator).FirstOrDefault();
        CorrectFloatDataValidator correctFloatDataValidator = (CorrectFloatDataValidator)(from validator in Validators where validator.GetType() == typeof(CorrectFloatDataValidator) select validator).FirstOrDefault();
        CorrectIntDataValidator correctIntDataValidator = (CorrectIntDataValidator)(from validator in Validators where validator.GetType() == typeof(CorrectIntDataValidator) select validator).FirstOrDefault();
        BookExistsValidator bookExistsValidator = (BookExistsValidator)(from validator in Validators where validator.GetType() == typeof(BookExistsValidator) select validator).FirstOrDefault();
        AuthorExistsValidator authorExistsValidator = (AuthorExistsValidator)(from validator in Validators where validator.GetType() == typeof(AuthorExistsValidator) select validator).FirstOrDefault();

        CommandAdd commandAdd = new CommandAdd(tableNameValidator, ErrorOutput, Managers);

        CommandView commandView = new CommandView(new IValidator[] {
            tableNameValidator
        }, ErrorOutput, Managers);

        CommandFind commandFind = new CommandFind(new IValidator[] {
            tableNameValidator, correctIntDataValidator, bookExistsValidator, authorExistsValidator
        }, ErrorOutput, Managers);

        CommandRemove commandRemove = new CommandRemove(new IValidator[] {
            tableNameValidator, correctIntDataValidator, bookExistsValidator, authorExistsValidator
        }, ErrorOutput, Managers);

        CommandStop commandStop = new CommandStop();

        CommandRegistration.RegisterCommand("add", commandAdd);
        CommandRegistration.RegisterCommand("view", commandView);
        CommandRegistration.RegisterCommand("find", commandFind);
        CommandRegistration.RegisterCommand("remove", commandRemove);
        CommandRegistration.RegisterCommand("stop", commandStop);
    }

    public ICommand GetCommandByName(string name)
    {
        ICommand command = CommandRegistration.CommandList[name].Command;
        return command;
    }

    public int GetCommandArgsCountByName(string name)
    {
        return CommandRegistration.CommandList[name].RequiredArgs.Count;
    }

    public bool IsCommandExists(string name)
    {
        if (CommandRegistration.CommandList.ContainsKey(name))
            return true;
        return false;
    }
}