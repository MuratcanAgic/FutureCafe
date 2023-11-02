using FluentValidation.Results;
using FutureCafe.Core.Results;
using FutureCafe.Entities.Concrete;
using System.Linq.Expressions;

namespace FutureCafe.Business.Abstract
{
  public interface IProductService
  {
    ValidationResult Validate(Product entity);
    IDataResult<Product> FindById(int id);
    IDataResult<Product> Get(Expression<Func<Product, bool>> filter, string includeProperties = "");
    IEnumerable<IDataResult<Product>> GetList(Expression<Func<Product, bool>> filter = null,
    Func<IQueryable<Product>, IOrderedQueryable<Product>> orderBy = null, string includeProperties = "");
    IDataResult<Product> Add(Product entity);
    IResult Update(Product entity);
    IResult Delete(Product entity);
    IResult DeleteById(int id);
    IResult Any(Expression<Func<Product, bool>> filter);
    IResult Save();
    IDataResult<int> CountWhere(Expression<Func<Product, bool>> filter);

    //Asnyc
    IDataResult<Task<Product>> FindByIdAsync(int id);
    IDataResult<Task<Product>> GetAsync(Expression<Func<Product, bool>> filter, string includeProperties = "");
    IDataResult<Task<IEnumerable<Product>>> GetListAsync(Expression<Func<Product, bool>> filter = null,
    Func<IQueryable<Product>, IOrderedQueryable<Product>> orderBy = null, string includeProperties = "");
    IDataResult<Task> AddAsync(Product entity);
    IDataResult<Task<int>> SaveAsync();
    IDataResult<Task<int>> CountWhereAsync(Expression<Func<Product, bool>> filter);
  }
}
