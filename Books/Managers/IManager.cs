public interface IManager
{
    void Add();
    void Remove(int ID);
    void List();
    void Find(int ID);
    CModel IsExists(int ID);
}