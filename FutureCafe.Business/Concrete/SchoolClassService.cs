using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using FutureCafe.Business.Abstract;
using FutureCafe.Business.Constants;
using FutureCafe.Core.Utilities.Business;
using FutureCafe.Core.Utilities.Results;
using FutureCafe.DataAccess.Abstract;
using FutureCafe.Entities.Concrete;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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

    public IDataResult<TDto> Add<TDto>(TDto dto)
    {
      try
      {
        var entity = _mapper.Map<TDto, SchoolClass>(dto);

        //business result
        IResult result = BusinessRules.Run();

        if (result != null)
        {
          return new ErrorDataResult<TDto>(dto, result.Message);
        }
        _schoolClassDal.Add(entity);
        return new SuccessDataResult<TDto>(dto);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<TDto>(e.Message);
      }
    }
    public async Task<IDataResult<TDto>> AddAsync<TDto>(TDto dto)
    {
      try
      {
        var entity = _mapper.Map<TDto, SchoolClass>(dto);

        //business result
        IResult result = BusinessRules.Run();

        if (result != null)
        {
          return new ErrorDataResult<TDto>(dto, result.Message);
        }
        await _schoolClassDal.AddAsync(entity);
        return new SuccessDataResult<TDto>(dto);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<TDto>(e.Message);
      }
    }

    public IResult Any(Expression<Func<SchoolClass, bool>> filter)
    {
      try
      {
        var exist = _schoolClassDal.Any(filter);

        if (exist == true) return new SuccessResult(Messages.DataExist);

        return new ErrorResult(Messages.DataNotExist);
      }
      catch (Exception e)
      {
        return new ErrorResult(e.Message);
      }
    }

    public IDataResult<int> CountWhere(Expression<Func<SchoolClass, bool>> filter)
    {
      throw new NotImplementedException();
    }

    public Task<IDataResult<int>> CountWhereAsync(Expression<Func<SchoolClass, bool>> filter)
    {
      throw new NotImplementedException();
    }

    public IResult Delete<TDto>(TDto dto)
    {
      try
      {
        var entity = _mapper.Map<TDto, SchoolClass>(dto);
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

    public IDataResult<TDto> FindById<TDto>(int id)
    {
      try
      {
        return new SuccessDataResult<TDto>(_mapper.Map<SchoolClass, TDto>(_schoolClassDal.FindById(id)));
      }
      catch (Exception e)
      {
        return new ErrorDataResult<TDto>(e.Message);
      }
    }

    public async Task<IDataResult<TDto>> FindByIdAsync<TDto>(int id)
    {
      try
      {
        var schoolClass = await _schoolClassDal.FindByIdAsync(id);
        var schoolClassDto = _mapper.Map<SchoolClass, TDto>(schoolClass);
        return new SuccessDataResult<TDto>(schoolClassDto);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<TDto>(e.Message);
      }
    }

    public IDataResult<TDto> Get<TDto>(Expression<Func<SchoolClass, bool>> filter, string includeProperties = "")
    {
      throw new NotImplementedException();
    }

    public Task<IDataResult<TDto>> GetAsync<TDto>(Expression<Func<SchoolClass, bool>> filter, string includeProperties = "")
    {
      throw new NotImplementedException();
    }

    public IDataResult<IEnumerable<TDto>> GetList<TDto>(Expression<Func<SchoolClass, bool>> filter = null, Func<IQueryable<SchoolClass>, IOrderedQueryable<SchoolClass>> orderBy = null, string includeProperties = "")
    {
      try
      {
        var schoolClassList = _schoolClassDal.GetList(filter, orderBy, includeProperties);
        var dto = schoolClassList.Select(e => _mapper.Map<SchoolClass, TDto>(e)).ToList();
        if (dto == null)
        {
          return new ErrorDataResult<IEnumerable<TDto>>(Messages.ListEmpty);
        }
        return new SuccessDataResult<IEnumerable<TDto>>(dto);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<IEnumerable<TDto>>(e.Message.ToString());
      }
    }
    public async Task<IDataResult<IEnumerable<TDto>>> GetListAsync<TDto>(Expression<Func<SchoolClass, bool>> filter = null, Func<IQueryable<SchoolClass>, IOrderedQueryable<SchoolClass>> orderBy = null, string includeProperties = "")
    {
      try
      {
        var schoolClassList = await _schoolClassDal.GetListAsync(filter, orderBy, includeProperties);
        var dto = schoolClassList.Select(e => _mapper.Map<SchoolClass, TDto>(e)).ToList();
        if (dto == null)
        {
          return new ErrorDataResult<IEnumerable<TDto>>(Messages.ListEmpty);
        }
        return new SuccessDataResult<IEnumerable<TDto>>(dto);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<IEnumerable<TDto>>(e.Message.ToString());
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
      catch (DbUpdateException ex) when (IsForeignKeyViolation(ex))
      {
        return new ErrorDataResult<SchoolClass>("Bu sınıfla ilişkili öğrenciler olduğundan, sınıf silinemez. Öncelikle öğrenciler silinmeli.");
      }
      catch (Exception e)
      {
        return new ErrorDataResult<SchoolClass>(e.Message);
      }
    }
    private bool IsForeignKeyViolation(DbUpdateException ex)
    {
      return ex?.InnerException is SqlException sqlException &&
             (sqlException.Number == 547 || sqlException.Number == 2601);
    }

    public IResult Update<TDto>(TDto dto)
    {
      try
      {
        //business result
        IResult result = BusinessRules.Run();
        var entity = _mapper.Map<TDto, SchoolClass>(dto);

        if (result != null)
        {
          return new ErrorDataResult<TDto>(dto, result.Message);
        }
        _schoolClassDal.Update(entity);
        return new SuccessDataResult<TDto>(dto);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<TDto>(e.Message);
      }
    }

    public IDataResult<ValidationResult> Validate<TDto>(TDto dto)
    {
      try
      {
        var entity = _mapper.Map<SchoolClass>(dto);

        var validationResult = _validator.Validate(entity);

        if (validationResult == null)
          return new ErrorDataResult<ValidationResult>(Messages.ValidationResultNull);

        return new SuccessDataResult<ValidationResult>(validationResult);

      }
      catch (Exception e)
      {
        return new ErrorDataResult<ValidationResult>(e.Message);
      }
    }
  }
}
