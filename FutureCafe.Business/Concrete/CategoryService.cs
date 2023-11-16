using FluentValidation;
using FluentValidation.Results;
using FutureCafe.Business.Abstract;
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
    public CategoryService(ICategoryDal categoryDal, IValidator<Category> validator)
    {
      _categoryDal = categoryDal;
      _validator = validator;
    }

    public IDataResult<Category> Add(Category entity)
    {
      throw new NotImplementedException();
    }

    public IDataResult<Task> AddAsync(Category entity)
    {
      throw new NotImplementedException();
    }

    public IResult Any(Expression<Func<Category, bool>> filter)
    {
      throw new NotImplementedException();
    }

    public IDataResult<int> CountWhere(Expression<Func<Category, bool>> filter)
    {
      throw new NotImplementedException();
    }

    public IDataResult<Task<int>> CountWhereAsync(Expression<Func<Category, bool>> filter)
    {
      throw new NotImplementedException();
    }

    public IResult Delete(Category entity)
    {
      throw new NotImplementedException();
    }

    public IResult DeleteById(int id)
    {
      throw new NotImplementedException();
    }

    public IDataResult<Category> FindById(int id)
    {
      throw new NotImplementedException();
    }

    public IDataResult<Task<Category>> FindByIdAsync(int id)
    {
      throw new NotImplementedException();
    }

    public IDataResult<Category> Get(Expression<Func<Category, bool>> filter, string includeProperties = "")
    {
      throw new NotImplementedException();
    }

    public IDataResult<Task<Category>> GetAsync(Expression<Func<Category, bool>> filter, string includeProperties = "")
    {
      throw new NotImplementedException();
    }

    public IDataResult<IEnumerable<Category>> GetList(Expression<Func<Category, bool>> filter = null, Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null, string includeProperties = "")
    {
      throw new NotImplementedException();
    }

    public IDataResult<Task<IEnumerable<Category>>> GetListAsync(Expression<Func<Category, bool>> filter = null, Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null, string includeProperties = "")
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

    public IResult Update(Category entity)
    {
      throw new NotImplementedException();
    }

    public ValidationResult Validate(Category entity)
    {
      return _validator.Validate(entity);
    }
  }
}
