using FutureCafe.Business.Dtos;
using FutureCafe.Core.Utilities.Results;
using FutureCafe.Entities.Concrete;
using System.Linq.Expressions;

namespace FutureCafe.Business.Abstract
{
  public interface ITradeService
  {
    IDataResult<TDto> FindById<TDto>(int id);
    IDataResult<TDto> Get<TDto>(Expression<Func<Trade, bool>> filter, string includeProperties = "");
    IDataResult<IEnumerable<TDto>> GetList<TDto>(Expression<Func<Trade, bool>> filter = null,
    Func<IQueryable<Trade>, IOrderedQueryable<Trade>> orderBy = null, string includeProperties = "");
    IDataResult<TDto> Add<TDto>(TDto dto);
    IResult Update(Trade trade);
    IResult Delete<TDto>(TDto dto);
    IResult DeleteById(int id);
    IResult Any(Expression<Func<Trade, bool>> filter);
    IResult Save();
    IDataResult<int> CountWhere(Expression<Func<Trade, bool>> filter);

    Task<IDataResult<TDto>> FindByIdAsync<TDto>(int id) where TDto : class;
    Task<IDataResult<TDto>> GetAsync<TDto>(Expression<Func<Trade, bool>> filter, string includeProperties = "");
    Task<IDataResult<IEnumerable<TDto>>> GetListAsync<TDto>(Expression<Func<Trade, bool>> filter = null, Func<IQueryable<Trade>, IOrderedQueryable<Trade>> orderBy = null, string includeProperties = "");
    /*Task<IDataResult<Trade>> AddAsync(Trade entity);*/
    Task<IDataResult<Trade>> AddAsync(string studentCardNumber, IEnumerable<ProductTradeDto> products);
    Task<IResult> SaveAsync();
    Task<IDataResult<int>> CountWhereAsync(Expression<Func<Trade, bool>> filter);
  }
}
