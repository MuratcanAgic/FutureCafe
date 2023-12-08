using FutureCafe.Core.Utilities.Results;
using FutureCafe.Entities.Concrete;
using System.Linq.Expressions;

namespace FutureCafe.Business.Abstract
{
	public interface IOperationClaimService
	{
		//IDataResult<ValidationResult> Validate<TDto>(TDto dto);
		IDataResult<TDto> FindById<TDto>(int id);
		IDataResult<TDto> Get<TDto>(Expression<Func<OperationClaim, bool>> filter, string includeProperties = "");
		IDataResult<IEnumerable<TDto>> GetList<TDto>(Expression<Func<OperationClaim, bool>> filter = null,
		Func<IQueryable<OperationClaim>, IOrderedQueryable<OperationClaim>> orderBy = null, string includeProperties = "");
		IDataResult<TDto> Add<TDto>(TDto dto);
		IResult Update<TDto>(TDto dto);
		IResult Delete<TDto>(TDto dto);
		IResult DeleteById(int id);
		IResult Any(Expression<Func<OperationClaim, bool>> filter);
		IResult Save();
		IDataResult<int> CountWhere(Expression<Func<OperationClaim, bool>> filter);

		//Asnyc
		Task<IDataResult<TDto>> FindByIdAsync<TDto>(int id);
		Task<IDataResult<TDto>> GetAsync<TDto>(Expression<Func<OperationClaim, bool>> filter, string includeProperties = "");
		Task<IDataResult<IEnumerable<TDto>>> GetListAsync<TDto>(Expression<Func<OperationClaim, bool>> filter = null, Func<IQueryable<OperationClaim>, IOrderedQueryable<OperationClaim>> orderBy = null, string includeProperties = "");
		/*Task<IDataResult<OperationClaim>> AddAsync(OperationClaim entity);*/
		Task<IDataResult<TDto>> AddAsync<TDto>(TDto dto);
		Task<IResult> SaveAsync();
		Task<IDataResult<int>> CountWhereAsync(Expression<Func<OperationClaim, bool>> filter);
	}
}
