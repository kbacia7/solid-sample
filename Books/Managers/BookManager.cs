using System;
using System.Linq;

public class BookManager : IManager
{
    private IValidator[] validators; //INt, float
    private Book NewBook;
    private InputReader InputReader;
    private BookContext BookContext;

    public BookManager(IValidator[] _validators, Book _newBook, InputReader _inputReader, BookContext _bookContext)
    {
        validators = _validators;
        NewBook = _newBook;
        InputReader = _inputReader;
        BookContext = _bookContext;
    }

    public void Add()
    {
        ValidatorResult validatorResult = null;
        Book copy = NewBook;

        Console.Write("Name: ");
        NewBook.Name = Console.ReadLine();

        for (; ; )
        {
            Console.Write("Price: ");
            string input = Console.ReadLine();
            validatorResult = validators[1].Validate(input);
            if (validatorResult.Success)
            {
                NewBook.Price = float.Parse(input);
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
                NewBook.Author_ID = int.Parse(input);
                break;
            }
        }
        BookContext.Books.Add(NewBook);
        BookContext.SaveChanges();
        NewBook = copy;
    }

    public void Remove(int ID)
    {
        BookContext.Books.Remove(BookContext.Books.Where(book => book.ID == ID).Select(b => b).First());
        Console.WriteLine("Remove book with ID " + ID);
        BookContext.SaveChanges();
    }

    public void List()
    {
        foreach(Book b in BookContext.Books)
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
        Book findBook = (from a in BookContext.Books where a.ID == ID select a).FirstOrDefault();
        return findBook;
    }
}