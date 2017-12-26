using System.Collections.Generic;


public class Command
{
    private BookContext BookContext;
    public Command(BookContext bookContext)
    {
        BookContext = bookContext;
    }

    public void ExecuteCommand(ICommand command, List<string> args)
    {
        LengthValidator lenValidator = new LengthValidator();
        ErrorOutput errorOutput = new ErrorOutput();
        string data = string.Join(" ", args);
        ValidatorResult res = lenValidator.Validate(data);
        if (res.Success)
        {
            args.RemoveAt(0);
            command.Execute(args, BookContext);
        }
        else
            errorOutput.ErrorParse(res.ErrorCode);
    }
}
