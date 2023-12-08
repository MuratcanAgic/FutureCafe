using FutureCafe.Core.DataAccess.Concrete;
using FutureCafe.DataAccess.Abstract;
using FutureCafe.DataAccess.Context;
using FutureCafe.Entities.Concrete;

namespace FutureCafe.DataAccess.Concrete.EntityFramework
{
	public class EfUserOperationClaimDal : EfEntityRepositoryBase<UserOperationClaim, DatabaseContext>, IUserOperationClaimDal
	{
		private DatabaseContext _dbContext;
		public EfUserOperationClaimDal(DatabaseContext context) : base(context)
		{
			_dbContext = context;
		}
	}
}
