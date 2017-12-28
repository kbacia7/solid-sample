public class TypeValidator : IValidator
{
    public ValidatorResult Validate(string type)
    {
        if (type.ToLower() != "book" && type.ToLower() != "author")
            return new ValidatorResult(ErrorCode.INVALID_TYPE);
        return new ValidatorResult(ErrorCode.NO_ERROR);
    }
}