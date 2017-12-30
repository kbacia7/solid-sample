public class ValidatorResult
{
    public readonly bool Success;
    public readonly ErrorCode ErrorCode;

    public ValidatorResult(ErrorCode error)
    {
        Success = (error != ErrorCode.NO_ERROR) ? (false) : (true);
        ErrorCode = error;
    }
}