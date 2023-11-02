using FutureCafe.Core.Entitites;
using FutureCafe.Entities.Abstract;

namespace FutureCafe.Entities.Concrete
{
  public class AuthorityPermission : BaseEntity, IEntity
  {
    public int AuthorityId { get; set; }
    public int PermissionId { get; set; }
    public Authority Authority { get; set; }
    public Permission Permission { get; set; }
  }
}
