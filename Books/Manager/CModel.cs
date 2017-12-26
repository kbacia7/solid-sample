public class CModel
{
    public virtual void Add(BookContext bookContext)
    {
        bookContext.SaveChanges();
    }

    public virtual void Remove(BookContext bookContext)
    {
        bookContext.SaveChanges();
    }
}

