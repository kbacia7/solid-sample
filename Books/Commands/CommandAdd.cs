using System;
using System.Collections.Generic;
using System.Reflection;

public class CommandAdd : ICommand
{
    public string Name { get { return "add"; } }
    public void Execute(List<string> args, BookContext bookContext)
    {
        TypeValidator typeValidator = new TypeValidator();
        ErrorOutput errorOutput = new ErrorOutput();
        string type = args[0];
        ValidatorResult validatorResult = typeValidator.Validate(type);
        if (validatorResult.Success)
        {
            Console.WriteLine("Create a new element");
            Console.WriteLine("Type: " + type);
            Type searchType = Type.GetType(type, true, true);
            dynamic sObj = Activator.CreateInstance(searchType);
            PropertyInfo[] properties = searchType.GetProperties();
            DataTypeValidator dTypeValidator = new DataTypeValidator();
            foreach (PropertyInfo property in properties)
            {
                if (dTypeValidator.Validate(property.PropertyType.ToString()).Success)
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
            errorOutput.ErrorParse(validatorResult.ErrorCode);
    }
}

