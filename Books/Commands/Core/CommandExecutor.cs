using System.Collections.Generic;

public class CommandExecutor
{
    public BookContext BookContext { get; set; } 
    public IErrorOutput ErrorOutput { get; set; }
    private IValidator Validator; //lengthValidator

    public CommandExecutor(IValidator validator)
    {
        Validator = validator;
    }

    public void ExecuteCommand(ICommand command, IList<string> args)
    {
        string data = string.Join(" ", args);
        ValidatorResult res = Validator.Validate(data);
        if (res.Success)
        {
            args.RemoveAt(0);
            command.Execute(args, BookContext);
        }
        else
            ErrorOutput.WriteError(res.ErrorCode);
    } 
}