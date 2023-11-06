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
    IDataResult<Task<Student>> FindByIdAsync(int id);
    IDataResult<Task<Student>> GetAsync(Expression<Func<Student, bool>> filter, string includeProperties = "");
    IDataResult<Task<IEnumerable<Student>>> GetListAsync(Expression<Func<Student, bool>> filter = null,
    Func<IQueryable<Student>, IOrderedQueryable<Student>> orderBy = null, string includeProperties = "");
    IDataResult<Task> AddAsync(Student entity);
    IDataResult<Task<int>> SaveAsync();
    IDataResult<Task<int>> CountWhereAsync(Expression<Func<Student, bool>> filter);
  }
}
