using FluentValidation.Results;
using FutureCafe.Core.Utilities.Results;
using FutureCafe.Entities.Concrete;
using System.Linq.Expressions;

namespace FutureCafe.Business.Abstract
{
  public interface IStudentService
  {
    IDataResult<ValidationResult> Validate<TDto>(TDto dto);
    IDataResult<TDto> FindById<TDto>(int id);
    IDataResult<TDto> Get<TDto>(Expression<Func<Student, bool>> filter, string includeProperties = "");
    IDataResult<IEnumerable<TDto>> GetList<TDto>(Expression<Func<Student, bool>> filter = null,
    Func<IQueryable<Student>, IOrderedQueryable<Student>> orderBy = null, string includeProperties = "");
    IDataResult<TDto> Add<TDto>(TDto dto);
    IResult Update<TDto>(TDto dto);
    IResult Delete<TDto>(TDto dto);
    IResult DeleteById(int id);
    IResult Any(Expression<Func<Student, bool>> filter);
    IResult Save();
    IDataResult<int> CountWhere(Expression<Func<Student, bool>> filter);

    //Asnyc
    Task<IDataResult<TDto>> FindByIdAsync<TDto>(int id);
    Task<IDataResult<TDto>> GetAsync<TDto>(Expression<Func<Student, bool>> filter, string includeProperties = "");
    Task<IDataResult<IEnumerable<TDto>>> GetListAsync<TDto>(Expression<Func<Student, bool>> filter = null, Func<IQueryable<Student>, IOrderedQueryable<Student>> orderBy = null, string includeProperties = "");
    /*Task<IDataResult<Student>> AddAsync(Student entity);*/
    Task<IDataResult<TDto>> AddAsync<TDto>(TDto dto);
    Task<IResult> SaveAsync();
    Task<IDataResult<int>> CountWhereAsync(Expression<Func<Student, bool>> filter);
  }
}
