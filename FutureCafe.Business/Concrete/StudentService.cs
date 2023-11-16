using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using FutureCafe.Business.Abstract;
using FutureCafe.Business.Constants;
using FutureCafe.Core.Utilities.Business;
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
    IMapper _mapper;
    public StudentService(IStudentDal studentDal, IValidator<Student> validator, IMapper mapper)
    {
      _studentDal = studentDal;
      _validator = validator;
      _mapper = mapper;
    }
    public IDataResult<Student> Add(Student entity)
    {
      try
      {
        //business result
        IResult result = BusinessRules.Run();

        if (result != null)
        {
          return new ErrorDataResult<Student>(entity, result.Message);
        }
        _studentDal.Add(entity);
        return new SuccessDataResult<Student>(entity);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<Student>(e.Message);
      }
    }

    public async Task<IDataResult<Student>> AddAsync(Student entity)
    {
      try
      {
        //business result
        IResult result = BusinessRules.Run();

        if (result != null)
        {
          return new ErrorDataResult<Student>(entity, result.Message);
        }
        await _studentDal.AddAsync(entity);
        return new SuccessDataResult<Student>(entity);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<Student>(e.Message);
      }
    }

    public IResult Any(Expression<Func<Student, bool>> filter)
    {
      throw new NotImplementedException();
    }

    public IDataResult<int> CountWhere(Expression<Func<Student, bool>> filter)
    {
      throw new NotImplementedException();
    }

    public Task<IDataResult<int>> CountWhereAsync(Expression<Func<Student, bool>> filter)
    {
      throw new NotImplementedException();
    }

    public IResult Delete(Student entity)
    {
      try
      {
        _studentDal.Delete(entity);
        return new SuccessDataResult<Student>(Messages.DataDeleted);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<Student>(e.Message);
      }
    }

    public IResult DeleteById(int id)
    {
      try
      {
        _studentDal.DeleteById(id);
        return new SuccessDataResult<Student>(Messages.DataDeleted);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<Student>(e.Message);
      }
    }

    public IDataResult<Student> FindById(int id)
    {
      try
      {
        return new SuccessDataResult<Student>(_studentDal.FindById(id));
      }
      catch (Exception e)
      {
        return new ErrorDataResult<Student>(e.Message);
      }
    }

    public async Task<IDataResult<Student>> FindByIdAsync(int id)
    {
      try
      {
        var student = await _studentDal.FindByIdAsync(id);
        return new SuccessDataResult<Student>(student);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<Student>(e.Message);
      }
    }

    public IDataResult<Student> Get(Expression<Func<Student, bool>> filter, string includeProperties = "")
    {

      throw new NotImplementedException();
    }

    public Task<IDataResult<Student>> GetAsync(Expression<Func<Student, bool>> filter, string includeProperties = "")
    {
      throw new NotImplementedException();
    }

    public IDataResult<IEnumerable<Student>> GetList(Expression<Func<Student, bool>> filter = null, Func<IQueryable<Student>, IOrderedQueryable<Student>> orderBy = null, string includeProperties = "")
    {
      try
      {
        var studentList = _studentDal.GetList(filter, orderBy, includeProperties);

        if (studentList == null)
        {
          return new ErrorDataResult<IEnumerable<Student>>(Messages.ListEmpty);
        }
        return new SuccessDataResult<IEnumerable<Student>>(studentList);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<IEnumerable<Student>>(e.Message.ToString());
      }
    }
    public async Task<IDataResult<IEnumerable<Student>>> GetListAsync(Expression<Func<Student, bool>> filter = null, Func<IQueryable<Student>, IOrderedQueryable<Student>> orderBy = null, string includeProperties = "")
    {
      try
      {
        var studentList = await _studentDal.GetListAsync(filter, orderBy, includeProperties);

        if (studentList == null)
        {
          return new ErrorDataResult<IEnumerable<Student>>(Messages.ListEmpty);
        }
        return new SuccessDataResult<IEnumerable<Student>>(studentList);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<IEnumerable<Student>>(e.Message.ToString());
      }
    }

    public IDataResult<Student> MapDtoToEntity<TDto, TEntity>(TDto dto)
    {
      try
      {
        var entity = _mapper.Map<TDto, Student>(dto);
        return new SuccessDataResult<Student>(entity);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<Student>(e.Message.ToString());
      }
    }

    public IDataResult<TDto> MapEntityToDto<TEntity, TDto>(Student entity)
    {
      try
      {
        var dto = _mapper.Map<Student, TDto>(entity);
        return new SuccessDataResult<TDto>(dto);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<TDto>(e.Message.ToString());
      }
    }

    public IResult Save()
    {
      try
      {
        _studentDal.Save();
        return new SuccessDataResult<Student>(Messages.DataSaved);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<Student>(e.Message);
      }
    }

    public async Task<IResult> SaveAsync()
    {
      try
      {
        await _studentDal.SaveAsync();
        return new SuccessDataResult<Student>(Messages.DataSaved);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<Student>(e.Message);
      }
    }

    public IResult Update(Student entity)
    {
      try
      {
        //business result
        IResult result = BusinessRules.Run();

        if (result != null)
        {
          return new ErrorDataResult<Student>(entity, result.Message);
        }
        _studentDal.Update(entity);
        return new SuccessDataResult<Student>(entity);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<Student>(e.Message);
      }
    }

    public ValidationResult Validate(Student entity)
    {
      return _validator.Validate(entity);
    }

    public async Task<ValidationResult> ValidateAsync(Student entity)
    {
      return await _validator.ValidateAsync(entity);
    }

  }
}
