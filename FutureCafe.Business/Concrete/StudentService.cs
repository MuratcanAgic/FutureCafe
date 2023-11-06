using FluentValidation;
using FluentValidation.Results;
using FutureCafe.Business.Abstract;
using FutureCafe.Core.Utilities.Results;
using FutureCafe.DataAccess.Abstract;
using FutureCafe.Entities.Concrete;
using System.Linq.Expressions;

namespace FutureCafe.Business.Concrete
{
  public class StudentService : IStudentService
  {
    IStudentDal _studentDal;
    IValidator<Student> _validator;
    public StudentService(IStudentDal studentDal, IValidator<Student> validator)
    {
      _studentDal = studentDal;
      _validator = validator;
    }

    public IDataResult<Student> Add(Student entity)
    {
      throw new NotImplementedException();
    }

    public IDataResult<Task> AddAsync(Student entity)
    {
      throw new NotImplementedException();
    }

    public IResult Any(Expression<Func<Student, bool>> filter)
    {
      throw new NotImplementedException();
    }

    public IDataResult<int> CountWhere(Expression<Func<Student, bool>> filter)
    {
      throw new NotImplementedException();
    }

    public IDataResult<Task<int>> CountWhereAsync(Expression<Func<Student, bool>> filter)
    {
      throw new NotImplementedException();
    }

    public IResult Delete(Student entity)
    {
      throw new NotImplementedException();
    }

    public IResult DeleteById(int id)
    {
      throw new NotImplementedException();
    }

    public IDataResult<Student> FindById(int id)
    {
      throw new NotImplementedException();
    }

    public IDataResult<Task<Student>> FindByIdAsync(int id)
    {
      throw new NotImplementedException();
    }

    public IDataResult<Student> Get(Expression<Func<Student, bool>> filter, string includeProperties = "")
    {
      throw new NotImplementedException();
    }

    public IDataResult<Task<Student>> GetAsync(Expression<Func<Student, bool>> filter, string includeProperties = "")
    {
      throw new NotImplementedException();
    }

    public IDataResult<IEnumerable<Student>> GetList(Expression<Func<Student, bool>> filter = null, Func<IQueryable<Student>, IOrderedQueryable<Student>> orderBy = null, string includeProperties = "")
    {
      throw new NotImplementedException();
    }

    public IDataResult<Task<IEnumerable<Student>>> GetListAsync(Expression<Func<Student, bool>> filter = null, Func<IQueryable<Student>, IOrderedQueryable<Student>> orderBy = null, string includeProperties = "")
    {
      throw new NotImplementedException();
    }

    public IResult Save()
    {
      throw new NotImplementedException();
    }

    public IDataResult<Task<int>> SaveAsync()
    {
      throw new NotImplementedException();
    }

    public IResult Update(Student entity)
    {
      throw new NotImplementedException();
    }

    public ValidationResult Validate(Student entity)
    {
      return _validator.Validate(entity);
    }
  }
}
