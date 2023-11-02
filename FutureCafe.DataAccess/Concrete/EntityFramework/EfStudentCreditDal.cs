using FutureCafe.Core.DataAccess.Concrete;
using FutureCafe.DataAccess.Abstract;
using FutureCafe.DataAccess.Context;
using FutureCafe.Entities.Concrete;

namespace FutureCafe.DataAccess.Concrete.EntityFramework
{
  public class EfStudentCreditDal : EfEntityRepositoryBase<StudentCredit, DatabaseContext>, IStudentCreditDal
  {
    private DatabaseContext _dbContext;
    public EfStudentCreditDal(DatabaseContext context) : base(context)
    {
      _dbContext = context;
    }
  }
}
