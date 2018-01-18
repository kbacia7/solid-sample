public class CorrectFloatDataValidator : IValidator
{
    public ValidatorResult Validate(string data)
    {
        if (float.TryParse(data, out var r))
            return new ValidatorResult(ErrorCode.NO_ERROR);
        return new ValidatorResult(ErrorCode.INVALID_DATA_TYPE);
    }
}