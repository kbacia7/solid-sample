using System;
using System.Collections.Generic;

public class ConsoleErrorOutput : IErrorOutput
{
    Dictionary<ErrorCode, string> Errors = new Dictionary<ErrorCode, string>()
    {
        {ErrorCode.INVALID_TYPE, "Invalid type!"},
        {ErrorCode.INVALID_LEN_ARGS, "Invalid number of args!"},
        {ErrorCode.INVALID_DATA_TYPE, "Invalid data type!" },
        {ErrorCode.COMMAND_NOT_EXISTS, "This command does not exist!" }
    };

    public void WriteError(ErrorCode errorCode)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(Errors[errorCode]);
        Console.ForegroundColor = ConsoleColor.White;
    }
}