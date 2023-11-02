using FutureCafe.Core.Entitites;
using FutureCafe.Entities.Abstract;

namespace FutureCafe.Entities.Concrete
{
  public class StudentCredit : BaseEntity, IEntity
  {
    public int StudentId { get; set; }
    public int CreditId { get; set; }

    public Student Student { get; set; }
    public Credit Credit { get; set; }
  }
}
