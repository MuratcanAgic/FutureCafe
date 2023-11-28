using FluentValidation.Results;
using FutureCafe.Core.Utilities.Results;
using FutureCafe.Entities.Concrete;
using System.Linq.Expressions;

namespace FutureCafe.Business.Abstract
{
  public interface IUserService
  {
    IDataResult<ValidationResult> Validate<TDto>(TDto dto);
    List<OperationClaim> GetClaims(User user);

    IDataResult<TDto> Add<TDto>(TDto dto);
    IDataResult<User> GetByMail(string email);
    IResult Update<TDto>(TDto dto);
    IResult Delete<TDto>(TDto dto);
    IResult DeleteById(int id);
    IResult Save();

    Task<IDataResult<IEnumerable<TDto>>> GetListAsync<TDto>(Expression<Func<User, bool>> filter = null, Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null, string includeProperties = "");
    Task<IDataResult<TDto>> FindByIdAsync<TDto>(int id);
    Task<IDataResult<TDto>> GetAsync<TDto>(Expression<Func<User, bool>> filter, string includeProperties = "");
    Task<IDataResult<TDto>> AddAsync<TDto>(TDto dto);
    Task<IResult> SaveAsync();
  }
}
