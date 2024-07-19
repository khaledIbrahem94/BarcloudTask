using BarcloudTask.Core;
using System.Linq.Expressions;

namespace BarcloudTask.Service.Interface;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<(int Total, IQueryable<T> EntityList)> GetAllByCondition(Expression<Func<T, bool>> expression, GridParamters gridParamters, params Expression<Func<T, object>>[] includesPara);
    Task<T?> GetByIdAsync(object id);
    Task<T> InsertAsync(T entity);
    Task<List<T>> InsertAsync(List<T> entity);
    Task UpdateAsync(T entity);
    Task<int> DeleteAsync(object id);
}