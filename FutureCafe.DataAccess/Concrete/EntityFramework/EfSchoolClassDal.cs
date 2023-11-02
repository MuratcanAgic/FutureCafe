using FutureCafe.Core.DataAccess.Concrete;
using FutureCafe.DataAccess.Abstract;
using FutureCafe.DataAccess.Context;
using FutureCafe.Entities.Concrete;

namespace FutureCafe.DataAccess.Concrete.EntityFramework
{
  public class EfSchoolClassDal : EfEntityRepositoryBase<SchoolClass, DatabaseContext>, ISchoolClassDal
  {
    private DatabaseContext _dbContext;
    public EfSchoolClassDal(DatabaseContext context) : base(context)
    {
      _dbContext = context;
    }
  }
}
