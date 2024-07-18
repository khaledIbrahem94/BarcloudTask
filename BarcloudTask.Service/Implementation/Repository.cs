using BarcloudTask.Core;
using BarcloudTask.DataBase;
using BarcloudTask.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace BarcloudTask.Service.Implementation;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly DBContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(DBContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<(int Total, IQueryable<T> EntityList)> GetAllByCondition(Expression<Func<T, bool>> expression, GridParamters gridParamters, params Expression<Func<T, object>>[] includesPara)
    {
        IQueryable<T> query = _context.Set<T>().AsNoTracking();
        int countForSkip = (gridParamters.PageIndex) * gridParamters.RowsNumber;
        Task<int> Total = query.Where(expression).CountAsync();

        string OrderStr = $"{gridParamters.OrderBy} {gridParamters.OrderEnum.ToString().ToLower()}";

        IQueryable<T> res = expression == null ? query.OrderBy(OrderStr)
  .Skip(countForSkip).Take(gridParamters.RowsNumber).AsNoTracking() : query.OrderBy(OrderStr)
  .Where(expression).Skip(countForSkip).Take(gridParamters.RowsNumber).AsNoTracking();


        if (includesPara != null)
        {
            foreach (Expression<Func<T, object>> include in includesPara)
                res = res.Include(include.AsPath()).AsNoTracking();
        }

        return (await Total, res);
    }

    public async Task<T?> GetByIdAsync(object id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<T> InsertAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    public async Task<List<T>> InsertAsync(List<T> entity)
    {
        await _dbSet.AddRangeAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(object id)
    {
        T? entityToDelete = await _dbSet.FindAsync(id);
        if (entityToDelete != null)
        {
            _dbSet.Remove(entityToDelete);
            return await _context.SaveChangesAsync();
        }

        return 0;
    }
}
