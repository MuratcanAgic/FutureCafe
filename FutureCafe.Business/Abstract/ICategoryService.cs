using FluentValidation.Results;
using FutureCafe.Core.Utilities.Results;
using FutureCafe.Entities.Concrete;
using System.Linq.Expressions;

namespace FutureCafe.Business.Abstract
{
  public interface ICategoryService
  {
    ValidationResult Validate(Category entity);
    IDataResult<Category> FindById(int id);
    IDataResult<Category> Get(Expression<Func<Category, bool>> filter, string includeProperties = "");
    IDataResult<IEnumerable<Category>> GetList(Expression<Func<Category, bool>> filter = null,
    Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null, string includeProperties = "");
    IDataResult<Category> Add(Category entity);
    IResult Update(Category entity);
    IResult Delete(Category entity);
    IResult DeleteById(int id);
    IResult Any(Expression<Func<Category, bool>> filter);
    IResult Save();
    IDataResult<int> CountWhere(Expression<Func<Category, bool>> filter);

    //Asnyc
    Task<IDataResult<Category>> FindByIdAsync(int id);
    Task<IDataResult<Category>> GetAsync(Expression<Func<Category, bool>> filter, string includeProperties = "");
    Task<IDataResult<IEnumerable<Category>>> GetListAsync(Expression<Func<Category, bool>> filter = null, Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null, string includeProperties = "");
    Task<IDataResult<Category>> AddAsync(Category entity);
    Task<IResult> SaveAsync();
    Task<IDataResult<int>> CountWhereAsync(Expression<Func<Category, bool>> filter);
    Task<ValidationResult> ValidateAsync(Category entity);
    IDataResult<TDto> MapEntityToDto<TEntity, TDto>(Category entity);
    IDataResult<Category> MapDtoToEntity<TDto, TEntity>(TDto dto);
  }
}
