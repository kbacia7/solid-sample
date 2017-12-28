using System.Collections.Generic;

public class CommandExecutor
{
    private BookContext BookContext;
    private IValidator validator; //lengthValidator
    private IErrorOutput errorOutput;
    public CommandExecutor(IValidator _validator, IErrorOutput _errorOutput, BookContext bookContext)
    {
        validator = _validator;
        errorOutput = _errorOutput;
        BookContext = bookContext;
    }

    public void ExecuteCommand(ICommand command, IList<string> args)
    {
        string data = string.Join(" ", args);
        ValidatorResult res = validator.Validate(data);
        if (res.Success)
        {
            args.RemoveAt(0);
            command.Execute(args, BookContext);
        }
        else
            errorOutput.WriteError(res.ErrorCode);
    } 
}