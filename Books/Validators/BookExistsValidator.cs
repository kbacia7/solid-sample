public class BookExistsValidator : IValidator
{
    private BookManager BookManager;

    public BookExistsValidator(BookManager _bookManager)
    {
        BookManager = _bookManager;
    }

    public ValidatorResult Validate(string ID)
    {
        if(BookManager.IsExists(int.Parse(ID)) != null)
            return new ValidatorResult(ErrorCode.NO_ERROR);
        return new ValidatorResult(ErrorCode.BOOK_NO_EXISTS);
    }
}

