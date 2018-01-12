using System.Collections.Generic;

public class CommandView : ICommand
{
    private IValidator[] validators;    //[0] => TypeValidator
    private IErrorOutput errorOutput;
    private IManager[] managers; //[0] => Author [1] => Book 

    public CommandView(IValidator[] _validators, IErrorOutput _errorOutput, IManager[] _managers)
    {
        validators = _validators;
        errorOutput = _errorOutput;
        managers = _managers;
    }

    public void Execute(IList<string> args, BookContext BookContext)
    {
        string type = args[0];
        ValidatorResult validatorResult = validators[0].Validate(type);
        if (validatorResult.Success)
        {
            if (type == "author")
                managers[0].List();
            else
                managers[1].List();
        }
        else
            errorOutput.WriteError(validatorResult.ErrorCode);
    }
}