namespace SAA.Services;

public interface IPersistenceService
{
    List<T> GetAll<T>(string resourceName);
    T GetById<T>(int id, string resourceName);
    void AddOrUpdate<T>(T entity, string resourceName);
    void Delete<T>(int id, string resourceName);
}