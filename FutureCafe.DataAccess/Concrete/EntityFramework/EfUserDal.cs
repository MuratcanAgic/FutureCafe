using FutureCafe.Core.DataAccess.Concrete;
using FutureCafe.DataAccess.Abstract;
using FutureCafe.DataAccess.Context;
using FutureCafe.Entities.Concrete;

namespace FutureCafe.DataAccess.Concrete.EntityFramework
{
  public class EfUserDal : EfEntityRepositoryBase<User, DatabaseContext>, IUserDal
  {
    private DatabaseContext _dbContext;
    public EfUserDal(DatabaseContext context) : base(context)
    {
      _dbContext = context;
    }

    public List<OperationClaim> GetClaims(User user)
    {
      return Get(a => a.Id == user.Id, "UserOperationClaims.OperationClaim").UserOperationClaims.Select(e => e.OperationClaim).ToList();
    }
  }
}
