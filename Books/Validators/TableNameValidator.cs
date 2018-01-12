public class TableNameValidator : IValidator
{
    public ValidatorResult Validate(string name)
    {
        if (name.ToLower() != "book" && name.ToLower() != "author")
            return new ValidatorResult(ErrorCode.INVALID_TABLE_NAME);
        return new ValidatorResult(ErrorCode.NO_ERROR);
    }
}