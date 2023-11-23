using FutureCafe.Core.Entitites;
using FutureCafe.Entities.Abstract;

namespace FutureCafe.Entities.Concrete
{
  public class UserOperationClaim : BaseEntity, IEntity
  {
    public int UserId { get; set; }
    public int OperationClaimId { get; set; }

    public User User { get; set; }
    public OperationClaim OperationClaim { get; set; }
  }
}
