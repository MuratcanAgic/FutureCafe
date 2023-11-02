using FutureCafe.Core.DataAccess.Concrete;
using FutureCafe.DataAccess.Abstract;
using FutureCafe.DataAccess.Context;
using FutureCafe.Entities.Concrete;

namespace FutureCafe.DataAccess.Concrete.EntityFramework
{
  public class EfStudentDal : EfEntityRepositoryBase<Student, DatabaseContext>, IStudentDal
  {
    private DatabaseContext _dbContext;
    public EfStudentDal(DatabaseContext context) : base(context)
    {
      _dbContext = context;
    }
  }
}
