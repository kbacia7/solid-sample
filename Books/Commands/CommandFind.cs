using System.Collections.Generic;

public class CommandFind : ICommand
{
    private IValidator[] validators;    //[0] => TypeValidator [1] => CorrectIntData,  [2] => BookExists, [3] => AuthorExists
    private IErrorOutput errorOutput;
    private IManager[] managers; //[0] => Author [1] => Book 

    public CommandFind(IValidator[] _validators, IErrorOutput _errorOutput, IManager[] _managers)
    {
        validators = _validators;
        errorOutput = _errorOutput;
        managers = _managers;
    }

    public void Execute(IList<string> args, BookContext BookContext)
    {
        string type = args[0];
        string ID = args[1];
        ValidatorResult validatorResult = validators[0].Validate(type);
        if (validatorResult.Success)
        {
            validatorResult = validators[1].Validate(ID);
            if (validatorResult.Success)
            {
                int IDint = int.Parse(ID);
                if (type == "author")
                {
                    validatorResult = validators[3].Validate(IDint.ToString());
                    if (validatorResult.Success)
                        managers[0].Find(IDint);
                    else
                        errorOutput.WriteError(validatorResult.ErrorCode);
                }
                else
                {
                    validatorResult = validators[2].Validate(IDint.ToString());
                    if (validatorResult.Success)
                        managers[1].Find(IDint);
                    else
                        errorOutput.WriteError(validatorResult.ErrorCode);
                }

            }
            else
                errorOutput.WriteError(validatorResult.ErrorCode);
        }
        else
            errorOutput.WriteError(validatorResult.ErrorCode);
    }
}