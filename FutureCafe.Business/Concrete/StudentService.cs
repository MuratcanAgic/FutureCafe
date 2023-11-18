﻿using AutoMapper;
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
    public IDataResult<TDto> Add<TDto>(TDto dto)
    {
      try
      {
        var entity = _mapper.Map<TDto, Student>(dto);

        //business result
        IResult result = BusinessRules.Run();

        if (result != null)
        {
          return new ErrorDataResult<TDto>(dto, result.Message);
        }
        _studentDal.Add(entity);
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
        var entity = _mapper.Map<TDto, Student>(dto);

        //business result
        IResult result = BusinessRules.Run();

        if (result != null)
        {
          return new ErrorDataResult<TDto>(dto, result.Message);
        }
        await _studentDal.AddAsync(entity);
        return new SuccessDataResult<TDto>(dto);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<TDto>(e.Message);
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

    public IResult Delete<TDto>(TDto dto)
    {
      try
      {
        var entity = _mapper.Map<TDto, Student>(dto);
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

    public IDataResult<TDto> FindById<TDto>(int id)
    {
      try
      {
        return new SuccessDataResult<TDto>(_mapper.Map<Student, TDto>(_studentDal.FindById(id)));
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
        var schoolClass = await _studentDal.FindByIdAsync(id);
        var schoolClassDto = _mapper.Map<Student, TDto>(schoolClass);
        return new SuccessDataResult<TDto>(schoolClassDto);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<TDto>(e.Message);
      }
    }

    public IDataResult<TDto> Get<TDto>(Expression<Func<Student, bool>> filter, string includeProperties = "")
    {
      throw new NotImplementedException();
    }

    public async Task<IDataResult<TDto>> GetAsync<TDto>(Expression<Func<Student, bool>> filter, string includeProperties = "")
    {
      try
      {
        var student = await _studentDal.GetAsync(filter, includeProperties);
        var studentDto = _mapper.Map<Student, TDto>(student);
        if (student == null)
        {
          return new ErrorDataResult<TDto>(Messages.ListEmpty);
        }
        return new SuccessDataResult<TDto>(studentDto);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<TDto>(e.Message.ToString());
      }
    }

    public IDataResult<IEnumerable<TDto>> GetList<TDto>(Expression<Func<Student, bool>> filter = null, Func<IQueryable<Student>, IOrderedQueryable<Student>> orderBy = null, string includeProperties = "")
    {
      try
      {
        var schoolClassList = _studentDal.GetList(filter, orderBy, includeProperties);
        var dto = schoolClassList.Select(e => _mapper.Map<Student, TDto>(e)).ToList();
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
    public async Task<IDataResult<IEnumerable<TDto>>> GetListAsync<TDto>(Expression<Func<Student, bool>> filter = null, Func<IQueryable<Student>, IOrderedQueryable<Student>> orderBy = null, string includeProperties = "")
    {
      try
      {
        var schoolClassList = await _studentDal.GetListAsync(filter, orderBy, includeProperties);
        var dto = schoolClassList.Select(e => _mapper.Map<Student, TDto>(e)).ToList();
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

    public IResult Update<TDto>(TDto dto)
    {
      try
      {
        //business result
        IResult result = BusinessRules.Run();
        var entity = _mapper.Map<TDto, Student>(dto);

        if (result != null)
        {
          return new ErrorDataResult<TDto>(dto, result.Message);
        }
        _studentDal.Update(entity);
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
        var entity = _mapper.Map<Student>(dto);

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
