namespace TP2.Lib
{
  public interface IRepository<T>
  {
    bool Insert(T data);
    T Find(long id);
    bool Save(T myObject);
    bool Delete(long id);
  }

}