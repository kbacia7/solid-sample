using System;
using System.Collections.Generic;

public class CommandExecutor
{
    private BookContext bookContext;
    private IValidator validator; //lengthValidator
    private IErrorOutput errorOutput;

    public CommandExecutor(IValidator _validator, IErrorOutput _errorOutput, BookContext bookContext)
    {
        validator = _validator;
        errorOutput = _errorOutput;
        this.bookContext = bookContext;
    }

    public void ExecuteCommand(ICommand command, IList<string> args)
    {
        string data = string.Join(" ", args);
        ValidatorResult res = validator.Validate(data);
        if (res.Success)
        {
            args.RemoveAt(0);
            Console.WriteLine(); //Space
            command.Execute(args, bookContext);
            Console.WriteLine();
        }
        else
            errorOutput.WriteError(res.ErrorCode);
    } 
}