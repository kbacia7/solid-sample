using System;

public class InputReader
{
    public string ReadInput()
    {
        string input = Console.ReadLine();
        if (input != null && input.Length > 0)
            return input;
        else
            return ReadInput();
    }
}