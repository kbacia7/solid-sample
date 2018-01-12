public class AuthorExistsValidator : IValidator
{
    private AuthorManager AuthorManager;

    public AuthorExistsValidator(AuthorManager _authorManager)
    {
        AuthorManager = _authorManager;
    }

    public ValidatorResult Validate(string ID)
    {
        if(AuthorManager.IsExists(int.Parse(ID)) != null)
            return new ValidatorResult(ErrorCode.NO_ERROR);
        return new ValidatorResult(ErrorCode.AUTHOR_NO_EXISTS);
    }
}

