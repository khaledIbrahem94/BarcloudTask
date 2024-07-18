namespace BarcloudTask.Service.Interface;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(object id);
    Task<T> InsertAsync(T entity);
    Task<List<T>> InsertAsync(List<T> entity);
    Task UpdateAsync(T entity);
    Task<int> DeleteAsync(object id);
}