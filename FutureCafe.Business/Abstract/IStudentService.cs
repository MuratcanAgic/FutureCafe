using FluentValidation.Results;
using FutureCafe.Core.Utilities.Results;
using FutureCafe.Entities.Concrete;
using System.Linq.Expressions;

namespace FutureCafe.Business.Abstract
{
  public interface IStudentService
  {
    ValidationResult Validate(Student entity);
    IDataResult<Student> FindById(int id);
    IDataResult<Student> Get(Expression<Func<Student, bool>> filter, string includeProperties = "");
    IDataResult<IEnumerable<Student>> GetList(Expression<Func<Student, bool>> filter = null,
    Func<IQueryable<Student>, IOrderedQueryable<Student>> orderBy = null, string includeProperties = "");
    IDataResult<Student> Add(Student entity);
    IResult Update(Student entity);
    IResult Delete(Student entity);
    IResult DeleteById(int id);
    IResult Any(Expression<Func<Student, bool>> filter);
    IResult Save();
    IDataResult<int> CountWhere(Expression<Func<Student, bool>> filter);

    //Asnyc
    Task<IDataResult<Student>> FindByIdAsync(int id);
    Task<IDataResult<Student>> GetAsync(Expression<Func<Student, bool>> filter, string includeProperties = "");
    Task<IDataResult<IEnumerable<Student>>> GetListAsync(Expression<Func<Student, bool>> filter = null, Func<IQueryable<Student>, IOrderedQueryable<Student>> orderBy = null, string includeProperties = "");
    Task<IDataResult<Student>> AddAsync(Student entity);
    Task<IResult> SaveAsync();
    Task<IDataResult<int>> CountWhereAsync(Expression<Func<Student, bool>> filter);
    Task<ValidationResult> ValidateAsync(Student entity);
    IDataResult<TDto> MapEntityToDto<TEntity, TDto>(Student entity);
    IDataResult<Student> MapDtoToEntity<TDto, TEntity>(TDto dto);
  }
}
