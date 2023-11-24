using FutureCafe.Core.DataAccess.Abstract;
using FutureCafe.Entities.Concrete;

namespace FutureCafe.DataAccess.Abstract
{
  public interface IUserDal : IEntityRepository<User>
  {
    List<OperationClaim> GetClaims(User user);
  }
}
