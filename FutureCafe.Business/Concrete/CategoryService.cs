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

    public IDataResult<Category> Add(Category entity)
    {
      try
      {
        //business result
        IResult result = BusinessRules.Run();
        if (result != null)
        {
          return new ErrorDataResult<Category>(entity, result.Message);
        }
        _categoryDal.Add(entity);
        return new SuccessDataResult<Category>(entity);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<Category>(e.Message);
      }
    }

    public async Task<IDataResult<Category>> AddAsync(Category entity)
    {
      try
      {
        //business result
        IResult result = BusinessRules.Run();

        if (result != null)
        {
          return new ErrorDataResult<Category>(entity, result.Message);
        }
        await _categoryDal.AddAsync(entity);
        return new SuccessDataResult<Category>(entity);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<Category>(e.Message);
      }
    }

    public IResult Any(Expression<Func<Category, bool>> filter)
    {
      throw new NotImplementedException();
    }

    public IDataResult<int> CountWhere(Expression<Func<Category, bool>> filter)
    {
      throw new NotImplementedException();
    }

    public Task<IDataResult<int>> CountWhereAsync(Expression<Func<Category, bool>> filter)
    {
      throw new NotImplementedException();
    }

    public IResult Delete(Category entity)
    {
      try
      {
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

    public IDataResult<Category> FindById(int id)
    {
      try
      {
        return new SuccessDataResult<Category>(_categoryDal.FindById(id));
      }
      catch (Exception e)
      {
        return new ErrorDataResult<Category>(e.Message);
      }
    }

    public async Task<IDataResult<Category>> FindByIdAsync(int id)
    {
      try
      {
        var category = await _categoryDal.FindByIdAsync(id);
        return new SuccessDataResult<Category>(category);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<Category>(e.Message);
      }
    }

    public IDataResult<Category> Get(Expression<Func<Category, bool>> filter, string includeProperties = "")
    {
      throw new NotImplementedException();
    }

    public Task<IDataResult<Category>> GetAsync(Expression<Func<Category, bool>> filter, string includeProperties = "")
    {
      throw new NotImplementedException();
    }

    public IDataResult<IEnumerable<Category>> GetList(Expression<Func<Category, bool>> filter = null, Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null, string includeProperties = "")
    {
      try
      {
        var categoryList = _categoryDal.GetList();

        if (categoryList == null)
        {
          return new ErrorDataResult<IEnumerable<Category>>(Messages.ListEmpty);
        }
        return new SuccessDataResult<IEnumerable<Category>>(categoryList);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<IEnumerable<Category>>(e.Message.ToString());
      }
    }

    public async Task<IDataResult<IEnumerable<Category>>> GetListAsync(Expression<Func<Category, bool>> filter = null, Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null, string includeProperties = "")
    {
      try
      {
        var categoryList = await _categoryDal.GetListAsync();

        if (categoryList == null)
        {
          return new ErrorDataResult<IEnumerable<Category>>(Messages.ListEmpty);
        }
        return new SuccessDataResult<IEnumerable<Category>>(categoryList);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<IEnumerable<Category>>(e.Message.ToString());
      }
    }

    public IDataResult<Category> MapDtoToEntity<TDto, TEntity>(TDto dto)
    {
      try
      {
        var entity = _mapper.Map<TDto, Category>(dto);
        return new SuccessDataResult<Category>(entity);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<Category>(e.Message.ToString());
      }
    }

    public IDataResult<TDto> MapEntityToDto<TEntity, TDto>(Category entity)
    {
      try
      {
        var dto = _mapper.Map<Category, TDto>(entity);
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
      catch (Exception e)
      {
        return new ErrorDataResult<Category>(e.Message);
      }
    }

    public IResult Update(Category entity)
    {
      try
      {
        //business result
        IResult result = BusinessRules.Run();

        if (result != null)
        {
          return new ErrorDataResult<Category>(entity, result.Message);
        }
        _categoryDal.Update(entity);
        return new SuccessDataResult<Category>(entity);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<Category>(e.Message);
      }
    }

    public ValidationResult Validate(Category entity)
    {
      return _validator.Validate(entity);
    }

    public async Task<ValidationResult> ValidateAsync(Category entity)
    {
      return await _validator.ValidateAsync(entity);
    }
  }
}
