using System;
using System.Linq;

public class BookManager : IManager
{
    private IValidator[] validators; //INt, float
    private Book newBook;
    private InputReader inputReader;
    private BookContext bookContext;

    public BookManager(IValidator[] _validators, Book _newBook, InputReader _inputReader, BookContext _bookContext)
    {
        validators = _validators;
        newBook = _newBook;
        inputReader = _inputReader;
        bookContext = _bookContext;
    }

    public void Add()
    {
        ValidatorResult validatorResult = null;
        Book copy = newBook;

        Console.Write("Name: ");
        newBook.Name = Console.ReadLine();

        for (; ; )
        {
            Console.Write("Price: ");
            string input = Console.ReadLine();
            validatorResult = validators[1].Validate(input);
            if (validatorResult.Success)
            {
                newBook.Price = float.Parse(input);
                break;
            }
        }

        for (; ; )
        {
            Console.Write("Author ID: ");
            string input = Console.ReadLine();
            validatorResult = validators[0].Validate(input);
            if (validatorResult.Success)
            {
                newBook.Author_ID = int.Parse(input);
                break;
            }
        }
        bookContext.Books.Add(newBook);
        bookContext.SaveChanges();
        newBook = copy;
    }

    public void Remove(int ID)
    {
        bookContext.Books.Remove(bookContext.Books.Where(book => book.ID == ID).Select(b => b).First());
        Console.WriteLine("Remove book with ID " + ID);
        bookContext.SaveChanges();
    }

    public void List()
    {
        foreach(Book b in bookContext.Books)
        {
            Console.WriteLine("ID: " + b.ID);
            Console.WriteLine("Name: " + b.Name);
            Console.WriteLine("Price: " + b.Price);
            Console.WriteLine("Author ID: " + b.Author_ID);
            Console.WriteLine();
        }
    }

    public void Find(int ID)
    {
        Book book = (Book)IsExists(ID);
        if (book != null)
        {
            Console.WriteLine("Name: " + book.Name);
            Console.WriteLine("Price: " + book.Price);
            Console.WriteLine("Author ID: " + book.Author_ID);
        }
    }

    public CModel IsExists(int ID)
    {
        Book findBook = (from a in bookContext.Books where a.ID == ID select a).FirstOrDefault();
        return findBook;
    }
}