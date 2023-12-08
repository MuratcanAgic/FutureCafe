using FutureCafe.Core.DataAccess.Concrete;
using FutureCafe.DataAccess.Abstract;
using FutureCafe.DataAccess.Context;
using FutureCafe.Entities.Concrete;

namespace FutureCafe.DataAccess.Concrete.EntityFramework
{
	public class EfOperationClaimDal : EfEntityRepositoryBase<OperationClaim, DatabaseContext>, IOperationClaimDal
	{
		private DatabaseContext _dbContext;
		public EfOperationClaimDal(DatabaseContext context) : base(context)
		{
			_dbContext = context;
		}
	}
}
