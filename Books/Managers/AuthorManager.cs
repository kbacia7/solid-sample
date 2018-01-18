using System;
using System.Linq;

public class AuthorManager : IManager
{
    private IValidator validator; //IntData
    public Author newAuthor { get; set; }
    public InputReader inputReader { get; set; }
    public BookContext bookContext { get; set; }

    public AuthorManager(IValidator _validator)
    {
        validator = _validator;
    }

    public void Add()
    {
        ValidatorResult validatorResult = null;
        Author copy = newAuthor;

        Console.Write("First Name: ");
        newAuthor.FirstName = inputReader.ReadInput();

        Console.Write("Last Name: ");
        newAuthor.LastName = inputReader.ReadInput();

        Console.Write("Age: ");
        newAuthor.Age = int.Parse(inputReader.ReadAs((string s) => {
            validatorResult = validator.Validate(s);
            if (validatorResult.Success)
                return true;
            return false;
        }));

        bookContext.Authors.Add(newAuthor);
        bookContext.SaveChanges();

        newAuthor = copy;
    }

    public void Remove(int ID)
    {
        bookContext.Authors.Remove(bookContext.Authors.Where(author => author.ID == ID).Select(auth => auth).First());
        Console.WriteLine("Remove author with ID " + ID);
        bookContext.SaveChanges();
    }

    public void List()
    {
        foreach (Author a in bookContext.Authors)
        {
            Console.WriteLine("ID: " + a.ID);
            Console.WriteLine("First Name: " + a.FirstName);
            Console.WriteLine("Last Name: " + a.LastName);
            Console.WriteLine("Age " + a.Age);
            Console.WriteLine();
        }
    }

    public void Find(int ID)
    {
        Author author = (Author)IsExists(ID);
        if (author != null)
        {
            Console.WriteLine("First Name: " + author.FirstName);
            Console.WriteLine("Last Name: " + author.LastName);
            Console.WriteLine("Age " + author.Age);
        }
    }

    public CModel IsExists(int ID)
    {
        Author findAuthor = (from a in bookContext.Authors where a.ID == ID select a).FirstOrDefault();
        return findAuthor;
    }
}