using System;


public class ErrorOutput
{
    void WriteError(string text)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(text);
        Console.ForegroundColor = ConsoleColor.White;
    }

    public void ErrorParse(ErrorCode errorCode)
    {
        switch(errorCode)
        {
            case ErrorCode.INVALID_TYPE:
                {
                    WriteError("Invalid type!");
                    break;
                }
            case ErrorCode.INVALID_LEN_ARGS:
                {
                    WriteError("Invalid number of args!");
                    break;
                }
            case ErrorCode.INVALID_FORMAT:
                {
                    WriteError("Invalid command format!");
                    break;
                }
            case ErrorCode.INVALID_DATA_TYPE:
                {
                    WriteError("Invalid data type!");
                    break;
                }
        }
    }
}

