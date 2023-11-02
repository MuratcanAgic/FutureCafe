using FluentValidation;
using FluentValidation.Results;
using FutureCafe.Business.Abstract;
using FutureCafe.Core.Results;
using FutureCafe.DataAccess.Abstract;
using FutureCafe.Entities.Concrete;
using System.Linq.Expressions;

namespace FutureCafe.Business.Concrete
{
  public class ProductService : IProductService
  {

    IProductDal _productDal;
    IValidator<Product> _validator;
    public ProductService(IProductDal productDal, IValidator<Product> validator)
    {
      _productDal = productDal;
      _validator = validator;
    }
    public IDataResult<Product> Add(Product entity)
    {
      throw new NotImplementedException();
    }

    public IDataResult<Task> AddAsync(Product entity)
    {
      throw new NotImplementedException();
    }

    public IResult Any(Expression<Func<Product, bool>> filter)
    {
      throw new NotImplementedException();
    }

    public IDataResult<int> CountWhere(Expression<Func<Product, bool>> filter)
    {
      throw new NotImplementedException();
    }

    public IDataResult<Task<int>> CountWhereAsync(Expression<Func<Product, bool>> filter)
    {
      throw new NotImplementedException();
    }

    public IResult Delete(Product entity)
    {
      throw new NotImplementedException();
    }

    public IResult DeleteById(int id)
    {
      throw new NotImplementedException();
    }

    public IDataResult<Product> FindById(int id)
    {
      throw new NotImplementedException();
    }

    public IDataResult<Task<Product>> FindByIdAsync(int id)
    {
      throw new NotImplementedException();
    }

    public IDataResult<Product> Get(Expression<Func<Product, bool>> filter, string includeProperties = "")
    {
      throw new NotImplementedException();
    }

    public IDataResult<Task<Product>> GetAsync(Expression<Func<Product, bool>> filter, string includeProperties = "")
    {
      throw new NotImplementedException();
    }

    public IEnumerable<IDataResult<Product>> GetList(Expression<Func<Product, bool>> filter = null, Func<IQueryable<Product>, IOrderedQueryable<Product>> orderBy = null, string includeProperties = "")
    {
      throw new NotImplementedException();
    }

    public IDataResult<Task<IEnumerable<Product>>> GetListAsync(Expression<Func<Product, bool>> filter = null, Func<IQueryable<Product>, IOrderedQueryable<Product>> orderBy = null, string includeProperties = "")
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

    public IResult Update(Product entity)
    {
      throw new NotImplementedException();
    }

    public ValidationResult Validate(Product entity)
    {
      return _validator.Validate(entity);
    }
  }
}
