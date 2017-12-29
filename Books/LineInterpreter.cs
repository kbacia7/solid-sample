using System.Collections.Generic;

public class LineInterpreter
{
    CommandSplit commandSplit;
    IValidator validator; //CommandExistsValidator
    IErrorOutput errorOutput;
    CommandManager commandManager;
    CommandExecutor commandExecutor;
    public LineInterpreter(IValidator _validator, IErrorOutput _errorOutput, CommandSplit _commandSplit, CommandManager _commandManager, CommandExecutor _commandExecutor)
    {
        validator = _validator;
        errorOutput = _errorOutput;
        commandSplit = _commandSplit;
        commandManager = _commandManager;
        commandExecutor = _commandExecutor;
    }
    public void Interpret(string line)
    {
        ValidatorResult validatorResult = null;
        List<string> data = (List<string>)commandSplit.Split(line);
        if (data != null && data.Count > 0)
        {
            string commandName = data[0];
            validatorResult = validator.Validate(commandName);
            if (validatorResult.Success)
            {
                ICommand commandToExecute = commandManager.GetCommandByName(commandName);
                commandExecutor.ExecuteCommand(commandToExecute, data);
            }
            else
                errorOutput.WriteError(validatorResult.ErrorCode);
        }
    }
}