using FluentValidation.Results;
using FutureCafe.Core.Results;
using FutureCafe.Entities.Concrete;
using System.Linq.Expressions;

namespace FutureCafe.Business.Abstract
{
  public interface ISchoolClassService
  {
    ValidationResult Validate(SchoolClass entity);
    IDataResult<SchoolClass> FindById(int id);
    IDataResult<SchoolClass> Get(Expression<Func<SchoolClass, bool>> filter, string includeProperties = "");
    IEnumerable<IDataResult<SchoolClass>> GetList(Expression<Func<SchoolClass, bool>> filter = null,
    Func<IQueryable<SchoolClass>, IOrderedQueryable<SchoolClass>> orderBy = null, string includeProperties = "");
    IDataResult<SchoolClass> Add(SchoolClass entity);
    IResult Update(SchoolClass entity);
    IResult Delete(SchoolClass entity);
    IResult DeleteById(int id);
    IResult Any(Expression<Func<SchoolClass, bool>> filter);
    IResult Save();
    IDataResult<int> CountWhere(Expression<Func<SchoolClass, bool>> filter);

    //Asnyc
    IDataResult<Task<SchoolClass>> FindByIdAsync(int id);
    IDataResult<Task<SchoolClass>> GetAsync(Expression<Func<SchoolClass, bool>> filter, string includeProperties = "");
    IDataResult<Task<IEnumerable<SchoolClass>>> GetListAsync(Expression<Func<SchoolClass, bool>> filter = null,
    Func<IQueryable<SchoolClass>, IOrderedQueryable<SchoolClass>> orderBy = null, string includeProperties = "");
    IDataResult<Task> AddAsync(SchoolClass entity);
    IDataResult<Task<int>> SaveAsync();
    IDataResult<Task<int>> CountWhereAsync(Expression<Func<SchoolClass, bool>> filter);
  }
}
