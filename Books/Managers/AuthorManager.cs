using System;
using System.Linq;

public class AuthorManager : IManager
{
    private IValidator validator; //INt
    private Author NewAuthor;
    private InputReader InputReader;
    private BookContext BookContext;

    public AuthorManager(IValidator _validator, Author _newAuthor, InputReader _inputReader, BookContext _bookContext)
    {
        validator = _validator;
        NewAuthor = _newAuthor;
        InputReader = _inputReader;
        BookContext = _bookContext;
    }

    public void Add()
    {
        ValidatorResult validatorResult = null;
        Author copy = NewAuthor;

        Console.Write("First Name: ");
        NewAuthor.FirstName = InputReader.ReadInput();

        Console.Write("Last Name: ");
        NewAuthor.LastName = InputReader.ReadInput();

        for (; ; )
        {
            Console.Write("Age: ");
            string input = InputReader.ReadInput();
            validatorResult = validator.Validate(input);
            if (validatorResult.Success)
            {
                NewAuthor.Age = int.Parse(input);
                break;
            }
        }
        BookContext.Authors.Add(NewAuthor);
        BookContext.SaveChanges();

        NewAuthor = copy;
    }

    public void Remove(int ID)
    {
        BookContext.Authors.Remove(BookContext.Authors.Where(author => author.ID == ID).Select(auth => auth).First());
        Console.WriteLine("Remove author with ID " + ID);
        BookContext.SaveChanges();
    }

    public void List()
    {
        foreach(Author a in BookContext.Authors)
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
        Author findAuthor = (from a in BookContext.Authors where a.ID == ID select a).FirstOrDefault();
        return findAuthor;
    }
}