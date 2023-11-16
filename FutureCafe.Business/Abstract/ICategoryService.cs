using FluentValidation.Results;
using FutureCafe.Core.Utilities.Results;
using FutureCafe.Entities.Concrete;
using System.Linq.Expressions;

namespace FutureCafe.Business.Abstract
{
  public interface ICategoryService
  {
    IDataResult<ValidationResult> Validate<TDto>(TDto dto);
    IDataResult<TDto> FindById<TDto>(int id);
    IDataResult<TDto> Get<TDto>(Expression<Func<Category, bool>> filter, string includeProperties = "");
    IDataResult<IEnumerable<TDto>> GetList<TDto>(Expression<Func<Category, bool>> filter = null,
    Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null, string includeProperties = "");
    IDataResult<TDto> Add<TDto>(TDto dto);
    IResult Update<TDto>(TDto dto);
    IResult Delete<TDto>(TDto dto);
    IResult DeleteById(int id);
    IResult Any(Expression<Func<Category, bool>> filter);
    IResult Save();
    IDataResult<int> CountWhere(Expression<Func<Category, bool>> filter);

    //Asnyc
    Task<IDataResult<TDto>> FindByIdAsync<TDto>(int id);
    Task<IDataResult<TDto>> GetAsync<TDto>(Expression<Func<Category, bool>> filter, string includeProperties = "");
    Task<IDataResult<IEnumerable<TDto>>> GetListAsync<TDto>(Expression<Func<Category, bool>> filter = null, Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null, string includeProperties = "");
    /*Task<IDataResult<Category>> AddAsync(Category entity);*/
    Task<IDataResult<TDto>> AddAsync<TDto>(TDto dto);
    Task<IResult> SaveAsync();
    Task<IDataResult<int>> CountWhereAsync(Expression<Func<Category, bool>> filter);
  }
}
