using FluentValidation.Results;
using FutureCafe.Core.Utilities.Results;
using FutureCafe.Entities.Concrete;
using System.Linq.Expressions;

namespace FutureCafe.Business.Abstract
{
  public interface ISchoolClassService
  {
    ValidationResult Validate(SchoolClass entity);
    IDataResult<SchoolClass> FindById(int id);
    IDataResult<SchoolClass> Get(Expression<Func<SchoolClass, bool>> filter, string includeProperties = "");
    IDataResult<IEnumerable<SchoolClass>> GetList(Expression<Func<SchoolClass, bool>> filter = null,
    Func<IQueryable<SchoolClass>, IOrderedQueryable<SchoolClass>> orderBy = null, string includeProperties = "");
    IDataResult<SchoolClass> Add(SchoolClass entity);
    IResult Update(SchoolClass entity);
    IResult Delete(SchoolClass entity);
    IResult DeleteById(int id);
    IResult Any(Expression<Func<SchoolClass, bool>> filter);
    IResult Save();
    IDataResult<int> CountWhere(Expression<Func<SchoolClass, bool>> filter);

    //Asnyc
    Task<IDataResult<SchoolClass>> FindByIdAsync(int id);
    Task<IDataResult<SchoolClass>> GetAsync(Expression<Func<SchoolClass, bool>> filter, string includeProperties = "");
    Task<IDataResult<IEnumerable<SchoolClass>>> GetListAsync(Expression<Func<SchoolClass, bool>> filter = null, Func<IQueryable<SchoolClass>, IOrderedQueryable<SchoolClass>> orderBy = null, string includeProperties = "");
    Task<IDataResult<SchoolClass>> AddAsync(SchoolClass entity);
    Task<IResult> SaveAsync();
    Task<IDataResult<int>> CountWhereAsync(Expression<Func<SchoolClass, bool>> filter);
    Task<ValidationResult> ValidateAsync(SchoolClass entity);
    IDataResult<TDto> MapEntityToDto<TEntity, TDto>(SchoolClass entity);
    IDataResult<SchoolClass> MapDtoToEntity<TDto, TEntity>(TDto dto);
  }
}
