using System;
using System.Collections.Generic;

public class ErrorOutput
{
    Dictionary<ErrorCode, string> Errors = new Dictionary<ErrorCode, string>()
    {
        {ErrorCode.INVALID_TYPE, "Invalid type!"},
        {ErrorCode.INVALID_LEN_ARGS, "Invalid number of args!"},
        {ErrorCode.INVALID_FORMAT, "Invalid command format!" },
        {ErrorCode.INVALID_DATA_TYPE, "Invalid data type!" },
        {ErrorCode.COMMAND_NOT_EXISTS, "This command does not exist!" }

    };

    void WriteError(string text)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(text);
        Console.ForegroundColor = ConsoleColor.White;
    }

    public void ErrorParse(ErrorCode errorCode)
    {
        WriteError(Errors[errorCode]);
    }
}

