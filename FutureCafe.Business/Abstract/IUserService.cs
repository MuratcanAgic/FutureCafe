using FluentValidation.Results;
using FutureCafe.Business.Dtos;
using FutureCafe.Core.Utilities.Results;
using FutureCafe.Entities.Concrete;
using System.Linq.Expressions;

namespace FutureCafe.Business.Abstract
{
  public interface IUserService
  {
    IDataResult<ValidationResult> Validate(UserForRegisterDto dto);
    List<OperationClaim> GetClaims(User user);

    IDataResult<UserForRegisterDto> Add(UserForRegisterDto dto);
    IDataResult<User> GetByMail(string email);
    IResult Update(UserForRegisterDto dto);
    IResult Delete<TDto>(TDto dto);
    IResult DeleteById(int id);
    IResult Save();
    IResult Any(Expression<Func<User, bool>> filter);

    Task<IDataResult<IEnumerable<TDto>>> GetListAsync<TDto>(Expression<Func<User, bool>> filter = null, Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null, string includeProperties = "");
    Task<IDataResult<TDto>> FindByIdAsync<TDto>(int id) where TDto : class;
    Task<IDataResult<TDto>> GetAsync<TDto>(Expression<Func<User, bool>> filter, string includeProperties = "");
    Task<IDataResult<UserForRegisterDto>> AddAsync(UserForRegisterDto dto);
    Task<IResult> SaveAsync();
  }
}
