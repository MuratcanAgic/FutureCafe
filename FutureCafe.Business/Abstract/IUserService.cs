using FutureCafe.Core.Utilities.Results;
using FutureCafe.Entities.Concrete;
using System.Linq.Expressions;

namespace FutureCafe.Business.Abstract
{
  public interface IUserService
  {
    List<OperationClaim> GetClaims(User user);
    void Add(User user);
    User GetByMail(string email);
    Task<IDataResult<IEnumerable<TDto>>> GetListAsync<TDto>(Expression<Func<User, bool>> filter = null, Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null, string includeProperties = "");
  }
}
