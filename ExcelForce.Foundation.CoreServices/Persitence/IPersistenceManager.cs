namespace ExcelForce.Foundation.CoreServices.Persitence
{
    public interface IPersistenceManager<T> where T : class
    {
        T Get();

        bool Set(T persitenceObject);

        bool Clear();
    }
}
