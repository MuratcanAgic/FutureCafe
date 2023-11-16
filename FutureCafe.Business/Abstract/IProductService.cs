using FluentValidation.Results;
using FutureCafe.Business.Dtos;
using FutureCafe.Core.Utilities.Results;
using FutureCafe.Entities.Concrete;
using System.Linq.Expressions;

namespace FutureCafe.Business.Abstract
{
  public interface IProductService
  {
    IDataResult<ValidationResult> Validate(ProductCreateEditDto dto);
    IDataResult<TDto> FindById<TDto>(int id);
    IDataResult<TDto> Get<TDto>(Expression<Func<Product, bool>> filter, string includeProperties = "");
    IDataResult<IEnumerable<TDto>> GetList<TDto>(Expression<Func<Product, bool>> filter = null,
    Func<IQueryable<Product>, IOrderedQueryable<Product>> orderBy = null, string includeProperties = "");
    IDataResult<TDto> Add<TDto>(TDto dto);
    IResult Update(ProductCreateEditDto dto);
    IResult Delete<TDto>(TDto dto);
    IResult DeleteById(int id);
    IResult Any(Expression<Func<Product, bool>> filter);
    IResult Save();
    IDataResult<int> CountWhere(Expression<Func<Product, bool>> filter);

    //Asnyc
    Task<IDataResult<TDto>> FindByIdAsync<TDto>(int id) where TDto : class;
    Task<IDataResult<TDto>> GetAsync<TDto>(Expression<Func<Product, bool>> filter, string includeProperties = "");
    Task<IDataResult<IEnumerable<TDto>>> GetListAsync<TDto>(Expression<Func<Product, bool>> filter = null, Func<IQueryable<Product>, IOrderedQueryable<Product>> orderBy = null, string includeProperties = "");
    /*Task<IDataResult<Product>> AddAsync(Product entity);*/
    Task<IDataResult<ProductCreateEditDto>> AddAsync(ProductCreateEditDto dto);
    Task<IResult> SaveAsync();
    Task<IDataResult<int>> CountWhereAsync(Expression<Func<Product, bool>> filter);
  }
}
