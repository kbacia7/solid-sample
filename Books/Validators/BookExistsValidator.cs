public class BookExistsValidator : IValidator
{
    public BookManager BookManager { get; set; }

    public ValidatorResult Validate(string ID)
    {
        if (BookManager.IsExists(int.Parse(ID)) != null)
            return new ValidatorResult(ErrorCode.NO_ERROR);
        return new ValidatorResult(ErrorCode.BOOK_NO_EXISTS);
    }
}
