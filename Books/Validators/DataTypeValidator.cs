public class DataTypeValidator : IValidator
{
    public ValidatorResult Validate(string type)
    {
        if (type == typeof(int).ToString() || type == typeof(float).ToString() || type == typeof(string).ToString())
            return new ValidatorResult(ErrorCode.NO_ERROR);
        return new ValidatorResult(ErrorCode.INVALID_DATA_TYPE);
    }
}

