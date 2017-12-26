public class CommandFormatValidator : IValidator
{
    public ValidatorResult Validate(string command)
    {
        if (command[0] != '/')
            return new ValidatorResult(ErrorCode.INVALID_FORMAT);
        return new ValidatorResult(ErrorCode.NO_ERROR);
    }
}
