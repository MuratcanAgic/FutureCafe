using System.Linq.Expressions;

namespace FutureCafe.Core.DataAccess.Abstract
{
  public interface IEntityRepository<T> where T : class, new()
  {
    T FindById(int id);
    T Get(Expression<Func<T, bool>> filter, string includeProperties = "");
    IEnumerable<T> GetList(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    void DeleteById(int id);
    bool Any(Expression<Func<T, bool>> filter);
    int Save();
    int CountWhere(Expression<Func<T, bool>> filter);

    //Asnyc
    Task<T> FindByIdAsync(int id);
    Task<T> GetAsync(Expression<Func<T, bool>> filter, string includeProperties = "");
    Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> filter = null,
    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
    Task AddAsync(T entity);
    Task<int> SaveAsync();
    Task<int> CountWhereAsync(Expression<Func<T, bool>> filter);
  }
}
