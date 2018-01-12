using System;
using System.Collections.Generic;
using System.Linq;

public class CommandManager
{
    private CommandRegistration commandRegistration;

    public CommandManager(IValidator[] validators, IErrorOutput errorOutput, IManager[] managers, CommandRegistration _commandRegistration)
    {
        commandRegistration = _commandRegistration;

        //Validators for commands
        TableNameValidator tableNameValidator = (TableNameValidator)(from validator in validators where validator.GetType() == typeof(TableNameValidator) select validator).FirstOrDefault();
        CorrectFloatDataValidator correctFloatDataValidator = (CorrectFloatDataValidator)(from validator in validators where validator.GetType() == typeof(CorrectFloatDataValidator) select validator).FirstOrDefault();
        CorrectIntDataValidator correctIntDataValidator = (CorrectIntDataValidator)(from validator in validators where validator.GetType() == typeof(CorrectIntDataValidator) select validator).FirstOrDefault();
        BookExistsValidator bookExistsValidator = (BookExistsValidator)(from validator in validators where validator.GetType() == typeof(BookExistsValidator) select validator).FirstOrDefault();
        AuthorExistsValidator authorExistsValidator = (AuthorExistsValidator)(from validator in validators where validator.GetType() == typeof(AuthorExistsValidator) select validator).FirstOrDefault();

        CommandAdd commandAdd = new CommandAdd(new IValidator[] {
            tableNameValidator, correctIntDataValidator, correctFloatDataValidator
        }, errorOutput, managers);

        CommandView commandView = new CommandView(new IValidator[] {
            tableNameValidator
        }, errorOutput, managers);

        CommandFind commandFind = new CommandFind(new IValidator[] {
            tableNameValidator, correctIntDataValidator, bookExistsValidator, authorExistsValidator
        }, errorOutput, managers);

        CommandRemove commandRemove = new CommandRemove(new IValidator[] {
            tableNameValidator, correctIntDataValidator, bookExistsValidator, authorExistsValidator
        }, errorOutput, managers);

        CommandStop commandStop = new CommandStop();

        commandRegistration.RegisterCommand("add", commandAdd);
        commandRegistration.RegisterCommand("view", commandView);
        commandRegistration.RegisterCommand("find", commandFind);
        commandRegistration.RegisterCommand("remove", commandRemove);
        commandRegistration.RegisterCommand("stop", commandStop);
    }

    public ICommand GetCommandByName(string name)
    {
        ICommand command = commandRegistration.CommandList[name].Command;
        return command;
    }

    public int GetCommandArgsCountByName(string name)
    {
        return commandRegistration.CommandList[name].RequiredArgs.Count;
    }

    public bool IsCommandExists(string name)
    {
        if (commandRegistration.CommandList.ContainsKey(name))
            return true;
        return false;
    }
}