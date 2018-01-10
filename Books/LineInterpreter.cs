using Ninject;
using System.Collections.Generic;

public class LineInterpreter
{
    [Inject]
    public CommandSplit CommandSplit { get; set; }

    [Inject]
    public IValidator validator { get; set; }//CommandExistsValidator

    [Inject]
    public IErrorOutput ErrorOutput { get; set; }

    [Inject]
    public CommandManager CommandManager { get; set; }

    [Inject]
    public CommandExecutor CommandExecutor { get; set; }


    public void Interpret(string line)
    {
        ValidatorResult validatorResult = null;
        List<string> data = (List<string>)CommandSplit.Split(line);
        if (data != null && data.Count > 0)
        {
            string commandName = data[0];
            validatorResult = validator.Validate(commandName);
            if (validatorResult.Success)
            {
                ICommand commandToExecute = CommandManager.GetCommandByName(commandName);
                CommandExecutor.ExecuteCommand(commandToExecute, data);
            }
            else
                ErrorOutput.WriteError(validatorResult.ErrorCode);
        }
    }
}