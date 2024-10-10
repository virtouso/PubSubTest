namespace Baman.CrudTest.Infrastructure;

public interface IRepository<T>
{
    bool Create(T item);
    T Read(Int64 id);
    bool Update(T item);
    bool Delete(Int64 id);
}