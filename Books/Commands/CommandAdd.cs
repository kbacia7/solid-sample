using System.Collections.Generic;

public class CommandAdd : ICommand
{
    private IValidator validator;    //[0] => TypeValidator
    private IManager[] managers; //[0] => AUTHOR, [1] => Book
    private IErrorOutput errorOutput;

    public CommandAdd(IValidator _validator, IErrorOutput _errorOutput, IManager[] _managers)
    {
        validator = _validator;
        errorOutput = _errorOutput;
        managers = _managers;
    }

    public void Execute(IList<string> args, BookContext bookContext)
    {
        string type = args[0];
        ValidatorResult validatorResult = validator.Validate(type);
        if (validatorResult.Success)
        {
            if (type == "author")
                managers[0].Add();
            else
                managers[1].Add();
        }
        else
            errorOutput.WriteError(validatorResult.ErrorCode);
    }
}