public class ValidatorResult
{
    public readonly bool Success;
    public readonly ErrorCode ErrorCode;
    public ValidatorResult(ErrorCode error)
    {
        if (error == ErrorCode.NO_ERROR)
            Success = true;
        else
            Success = false;
        ErrorCode = error;
    }
    
}

