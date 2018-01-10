using Ninject;
using System.Collections.Generic;

public class CommandExecutor
{
    [Inject]
    public BookContext BookContext { get; set; }

    [Inject]
    public IErrorOutput ErrorOutput { get; set; }

    [Inject]
    public IValidator Validator { get; set; } //lengthValidator

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