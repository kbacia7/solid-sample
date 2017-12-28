using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Author : CModel
{
    [Key]
    public int ID { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public virtual List<Book> Books { get; set; }

    public override void Add(BookContext bookContext)
    {
        bookContext.Authors.Add(this);
        base.Add(bookContext);
    }

    public override void Remove(BookContext bookContext)
    {
        bookContext.Authors.Remove(this);
        base.Remove(bookContext);
    }
}