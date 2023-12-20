﻿using AutoMapper;
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
  public class CategoryService : ICategoryService
  {
    ICategoryDal _categoryDal;
    IValidator<Category> _validator;
    IMapper _mapper;

    public CategoryService(ICategoryDal categoryDal, IValidator<Category> validator, IMapper mapper)
    {
      _categoryDal = categoryDal;
      _validator = validator;
      _mapper = mapper;
    }

    public IDataResult<TDto> Add<TDto>(TDto dto)
    {
      try
      {
        var entity = _mapper.Map<TDto, Category>(dto);

        //business result
        IResult result = BusinessRules.Run();

        if (result != null)
        {
          return new ErrorDataResult<TDto>(dto, result.Message);
        }
        _categoryDal.Add(entity);
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
        var entity = _mapper.Map<TDto, Category>(dto);

        //business result
        IResult result = BusinessRules.Run();

        if (result != null)
        {
          return new ErrorDataResult<TDto>(dto, result.Message);
        }
        await _categoryDal.AddAsync(entity);
        return new SuccessDataResult<TDto>(dto);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<TDto>(e.Message);
      }
    }

    public IResult Any(Expression<Func<Category, bool>> filter)
    {
      try
      {
        var exist = _categoryDal.Any(filter);

        if (exist == true) return new SuccessResult(Messages.DataExist);

        return new ErrorResult(Messages.DataNotExist);
      }
      catch (Exception e)
      {
        return new ErrorResult(e.Message);
      }
    }

    public IDataResult<int> CountWhere(Expression<Func<Category, bool>> filter)
    {
      throw new NotImplementedException();
    }

    public Task<IDataResult<int>> CountWhereAsync(Expression<Func<Category, bool>> filter)
    {
      throw new NotImplementedException();
    }

    public IResult Delete<TDto>(TDto dto)
    {
      try
      {
        var entity = _mapper.Map<TDto, Category>(dto);
        _categoryDal.Delete(entity);
        return new SuccessDataResult<Category>(Messages.DataDeleted);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<Category>(e.Message);
      }
    }

    public IResult DeleteById(int id)
    {
      try
      {
        _categoryDal.DeleteById(id);
        return new SuccessDataResult<Category>(Messages.DataDeleted);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<Category>(e.Message);
      }
    }

    public IDataResult<TDto> FindById<TDto>(int id)
    {
      try
      {
        return new SuccessDataResult<TDto>(_mapper.Map<Category, TDto>(_categoryDal.FindById(id)));
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
        var schoolClass = await _categoryDal.FindByIdAsync(id);
        var schoolClassDto = _mapper.Map<Category, TDto>(schoolClass);
        return new SuccessDataResult<TDto>(schoolClassDto);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<TDto>(e.Message);
      }
    }

    public IDataResult<TDto> Get<TDto>(Expression<Func<Category, bool>> filter, string includeProperties = "")
    {
      throw new NotImplementedException();
    }

    public Task<IDataResult<TDto>> GetAsync<TDto>(Expression<Func<Category, bool>> filter, string includeProperties = "")
    {
      throw new NotImplementedException();
    }

    public IDataResult<IEnumerable<TDto>> GetList<TDto>(Expression<Func<Category, bool>> filter = null, Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null, string includeProperties = "")
    {
      try
      {
        var schoolClassList = _categoryDal.GetList(filter, orderBy, includeProperties);
        var dto = schoolClassList.Select(e => _mapper.Map<Category, TDto>(e)).ToList();
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
    public async Task<IDataResult<IEnumerable<TDto>>> GetListAsync<TDto>(Expression<Func<Category, bool>> filter = null, Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null, string includeProperties = "")
    {
      try
      {
        var schoolClassList = await _categoryDal.GetListAsync(filter, orderBy, includeProperties);
        var dto = schoolClassList.Select(e => _mapper.Map<Category, TDto>(e)).ToList();
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
        _categoryDal.Save();
        return new SuccessDataResult<Category>(Messages.DataSaved);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<Category>(e.Message);
      }
    }

    public async Task<IResult> SaveAsync()
    {
      try
      {
        await _categoryDal.SaveAsync();
        return new SuccessDataResult<Category>(Messages.DataSaved);
      }
      catch (DbUpdateException ex) when (IsForeignKeyViolation(ex))
      {
        return new ErrorDataResult<SchoolClass>("Bu kategoriyle ilişkili ürünler olduğundan, kategori silinemez. Öncelikle ürünler silinmeli.");
      }
      catch (Exception e)
      {
        return new ErrorDataResult<Category>(e.Message);
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
        var entity = _mapper.Map<TDto, Category>(dto);

        if (result != null)
        {
          return new ErrorDataResult<TDto>(dto, result.Message);
        }
        _categoryDal.Update(entity);
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
        var entity = _mapper.Map<Category>(dto);

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
