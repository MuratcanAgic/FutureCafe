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
  public class SchoolClassService : ISchoolClassService
  {
    ISchoolClassDal _schoolClassDal;
    IValidator<SchoolClass> _validator;
    IMapper _mapper;
    public SchoolClassService(ISchoolClassDal schoolClassDal, IValidator<SchoolClass> validator, IMapper mapper)
    {
      _schoolClassDal = schoolClassDal;
      _validator = validator;
      _mapper = mapper;
    }

    public IDataResult<SchoolClass> Add(SchoolClass entity)
    {
      try
      {
        //business result
        IResult result = BusinessRules.Run();

        if (result != null)
        {
          return new ErrorDataResult<SchoolClass>(entity, result.Message);
        }
        _schoolClassDal.Add(entity);
        return new SuccessDataResult<SchoolClass>(entity);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<SchoolClass>(e.Message);
      }
    }

    public async Task<IDataResult<SchoolClass>> AddAsync(SchoolClass entity)
    {
      try
      {
        //business result
        IResult result = BusinessRules.Run();

        if (result != null)
        {
          return new ErrorDataResult<SchoolClass>(entity, result.Message);
        }
        await _schoolClassDal.AddAsync(entity);
        return new SuccessDataResult<SchoolClass>(entity);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<SchoolClass>(e.Message);
      }
    }

    public IResult Any(Expression<Func<SchoolClass, bool>> filter)
    {
      throw new NotImplementedException();
    }

    public IDataResult<int> CountWhere(Expression<Func<SchoolClass, bool>> filter)
    {
      throw new NotImplementedException();
    }

    public Task<IDataResult<int>> CountWhereAsync(Expression<Func<SchoolClass, bool>> filter)
    {
      throw new NotImplementedException();
    }

    public IResult Delete(SchoolClass entity)
    {
      try
      {
        _schoolClassDal.Delete(entity);
        return new SuccessDataResult<SchoolClass>(Messages.DataDeleted);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<SchoolClass>(e.Message);
      }
    }

    public IResult DeleteById(int id)
    {
      try
      {
        _schoolClassDal.DeleteById(id);
        return new SuccessDataResult<SchoolClass>(Messages.DataDeleted);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<SchoolClass>(e.Message);
      }
    }

    public IDataResult<SchoolClass> FindById(int id)
    {
      try
      {
        return new SuccessDataResult<SchoolClass>(_schoolClassDal.FindById(id));
      }
      catch (Exception e)
      {
        return new ErrorDataResult<SchoolClass>(e.Message);
      }
    }

    public async Task<IDataResult<SchoolClass>> FindByIdAsync(int id)
    {
      try
      {
        var schoolClass = await _schoolClassDal.FindByIdAsync(id);
        return new SuccessDataResult<SchoolClass>(schoolClass);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<SchoolClass>(e.Message);
      }
    }

    public IDataResult<SchoolClass> Get(Expression<Func<SchoolClass, bool>> filter, string includeProperties = "")
    {

      throw new NotImplementedException();
    }

    public Task<IDataResult<SchoolClass>> GetAsync(Expression<Func<SchoolClass, bool>> filter, string includeProperties = "")
    {
      throw new NotImplementedException();
    }

    public IDataResult<IEnumerable<SchoolClass>> GetList(Expression<Func<SchoolClass, bool>> filter = null, Func<IQueryable<SchoolClass>, IOrderedQueryable<SchoolClass>> orderBy = null, string includeProperties = "")
    {
      try
      {
        var schoolClassList = _schoolClassDal.GetList();

        if (schoolClassList == null)
        {
          return new ErrorDataResult<IEnumerable<SchoolClass>>(Messages.ListEmpty);
        }
        return new SuccessDataResult<IEnumerable<SchoolClass>>(schoolClassList);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<IEnumerable<SchoolClass>>(e.Message.ToString());
      }
    }
    public async Task<IDataResult<IEnumerable<SchoolClass>>> GetListAsync(Expression<Func<SchoolClass, bool>> filter = null, Func<IQueryable<SchoolClass>, IOrderedQueryable<SchoolClass>> orderBy = null, string includeProperties = "")
    {
      try
      {
        var schoolClassList = await _schoolClassDal.GetListAsync();

        if (schoolClassList == null)
        {
          return new ErrorDataResult<IEnumerable<SchoolClass>>(Messages.ListEmpty);
        }
        return new SuccessDataResult<IEnumerable<SchoolClass>>(schoolClassList);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<IEnumerable<SchoolClass>>(e.Message.ToString());
      }
    }

    public IDataResult<SchoolClass> MapDtoToEntity<TDto, TEntity>(TDto dto)
    {
      try
      {
        var entity = _mapper.Map<TDto, SchoolClass>(dto);
        return new SuccessDataResult<SchoolClass>(entity);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<SchoolClass>(e.Message.ToString());
      }
    }

    public IDataResult<TDto> MapEntityToDto<TEntity, TDto>(SchoolClass entity)
    {
      try
      {
        var dto = _mapper.Map<SchoolClass, TDto>(entity);
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
        _schoolClassDal.Save();
        return new SuccessDataResult<SchoolClass>(Messages.DataSaved);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<SchoolClass>(e.Message);
      }
    }

    public async Task<IResult> SaveAsync()
    {
      try
      {
        await _schoolClassDal.SaveAsync();
        return new SuccessDataResult<SchoolClass>(Messages.DataSaved);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<SchoolClass>(e.Message);
      }
    }

    public IResult Update(SchoolClass entity)
    {
      try
      {
        //business result
        IResult result = BusinessRules.Run();

        if (result != null)
        {
          return new ErrorDataResult<SchoolClass>(entity, result.Message);
        }
        _schoolClassDal.Update(entity);
        return new SuccessDataResult<SchoolClass>(entity);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<SchoolClass>(e.Message);
      }
    }

    public ValidationResult Validate(SchoolClass entity)
    {
      return _validator.Validate(entity);
    }

    public async Task<ValidationResult> ValidateAsync(SchoolClass entity)
    {
      return await _validator.ValidateAsync(entity);
    }
  }
}
