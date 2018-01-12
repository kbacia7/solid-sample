using System.Collections.Generic;

public class CommandAdd : ICommand
{
    private IValidator[] validators;    //[0] => TypeValidator,  [1] => CorrectIntDataValidator [2] => CorrectFloatDataValidator
    private IManager[] managers; //[0] => AUTHOR, [1] => Book
    private IErrorOutput errorOutput;

    public CommandAdd(IValidator[] _validators, IErrorOutput _errorOutput, IManager[] _managers)
    {
        validators = _validators;
        errorOutput = _errorOutput;
        managers = _managers;
    }

    public void Execute(IList<string> args, BookContext bookContext)
    {
        string type = args[0];
        IValidator[] validatorsManager = { validators[1], validators[2] };
        ValidatorResult validatorResult = validators[0].Validate(type);
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