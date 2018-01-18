public class AuthorExistsValidator : IValidator
{
    public AuthorManager AuthorManager { get; set; }

    public ValidatorResult Validate(string ID)
    {
        if (AuthorManager.IsExists(int.Parse(ID)) != null)
            return new ValidatorResult(ErrorCode.NO_ERROR);
        return new ValidatorResult(ErrorCode.AUTHOR_NO_EXISTS);
    }
}
