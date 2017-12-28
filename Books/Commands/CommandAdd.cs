using System;
using System.Collections.Generic;
using System.Reflection;

public class CommandAdd : ICommand
{
    IValidator[] validators;    //[0] => TypeValidator,  [1] => DataTypeValidator
    IErrorOutput errorOutput;
    public CommandAdd(IValidator[] _validators, IErrorOutput _errorOutput)
    {
        validators = _validators;
        errorOutput = _errorOutput;
    }

    public void Execute(IList<string> args, BookContext bookContext)
    {
        string type = args[0];
        ValidatorResult validatorResult = validators[0].Validate(type);
        if (validatorResult.Success)
        {
            Console.WriteLine("Create a new element");
            Console.WriteLine("Type: " + type);
            Type searchType = Type.GetType(type, true, true);
            dynamic sObj = Activator.CreateInstance(searchType);
            PropertyInfo[] properties = searchType.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (validators[1].Validate(property.PropertyType.ToString()).Success)
                {
                    Console.Write(string.Format("{0}: ", property.Name));
                    string value = Console.ReadLine();
                    object oValue = Convert.ChangeType(value, property.PropertyType);
                    property.SetValue(sObj, oValue);
                }
            }
            sObj.Add(bookContext);
        }
        else
            errorOutput.WriteError(validatorResult.ErrorCode);
    }
}       