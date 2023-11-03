using FluentValidation;
using FluentValidation.Results;
using FutureCafe.Business.Abstract;
using FutureCafe.Core.Utilities.Results;
using FutureCafe.DataAccess.Abstract;
using FutureCafe.Entities.Concrete;
using System.Linq.Expressions;

namespace FutureCafe.Business.Concrete
{
  public class SchoolClassService : ISchoolClassService
  {
    ISchoolClassDal _schoolClassDal;
    IValidator<SchoolClass> _validator;
    public SchoolClassService(ISchoolClassDal schoolClassDal, IValidator<SchoolClass> validator)
    {
      _schoolClassDal = schoolClassDal;
      _validator = validator;
    }


    public IDataResult<SchoolClass> Add(SchoolClass entity)
    {
      throw new NotImplementedException();
    }

    public IDataResult<Task> AddAsync(SchoolClass entity)
    {
      throw new NotImplementedException();
    }

    public IResult Any(Expression<Func<SchoolClass, bool>> filter)
    {
      throw new NotImplementedException();
    }

    public IDataResult<int> CountWhere(Expression<Func<SchoolClass, bool>> filter)
    {
      throw new NotImplementedException();
    }

    public IDataResult<Task<int>> CountWhereAsync(Expression<Func<SchoolClass, bool>> filter)
    {
      throw new NotImplementedException();
    }

    public IResult Delete(SchoolClass entity)
    {
      throw new NotImplementedException();
    }

    public IResult DeleteById(int id)
    {
      throw new NotImplementedException();
    }

    public IDataResult<SchoolClass> FindById(int id)
    {
      throw new NotImplementedException();
    }

    public IDataResult<Task<SchoolClass>> FindByIdAsync(int id)
    {
      throw new NotImplementedException();
    }

    public IDataResult<SchoolClass> Get(Expression<Func<SchoolClass, bool>> filter, string includeProperties = "")
    {

      throw new NotImplementedException();
    }

    public IDataResult<Task<SchoolClass>> GetAsync(Expression<Func<SchoolClass, bool>> filter, string includeProperties = "")
    {
      throw new NotImplementedException();
    }

    public IDataResult<IEnumerable<SchoolClass>> GetList(Expression<Func<SchoolClass, bool>> filter = null, Func<IQueryable<SchoolClass>, IOrderedQueryable<SchoolClass>> orderBy = null, string includeProperties = "")
    {
      try
      {
        var schoolClassList = _schoolClassDal.GetList();
        if (schoolClassList != null)
        {
          return new SuccessDataResult<IEnumerable<SchoolClass>>(schoolClassList, "Success");
        }
        else
        {
          return new ErrorDataResult<IEnumerable<SchoolClass>>("Class list empty");
        }
      }
      catch (Exception e)
      {
        return new ErrorDataResult<IEnumerable<SchoolClass>>(e.Message.ToString());

      }

    }

    public IDataResult<Task<IEnumerable<SchoolClass>>> GetListAsync(Expression<Func<SchoolClass, bool>> filter = null, Func<IQueryable<SchoolClass>, IOrderedQueryable<SchoolClass>> orderBy = null, string includeProperties = "")
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

    public IResult Update(SchoolClass entity)
    {
      throw new NotImplementedException();
    }

    public ValidationResult Validate(SchoolClass entity)
    {
      return _validator.Validate(entity);
    }
  }
}
