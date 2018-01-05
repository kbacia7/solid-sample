using System.Collections.Generic;

public class LineInterpreter
{
    public CommandSplit CommandSplit { get; set; }
    public IValidator validator { get; set; }//CommandExistsValidator
    public IErrorOutput ErrorOutput { get; set; }
    public CommandManager CommandManager { get; set; }
    public CommandExecutor CommandExecutor { get; set; }


    public LineInterpreter(IValidator _validator)
    {
        validator = _validator;
    }

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