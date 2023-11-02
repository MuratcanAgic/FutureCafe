using FutureCafe.Core.Entitites;
using FutureCafe.Entities.Abstract;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FutureCafe.Entities.Concrete
{
  public class Permission : BaseEntity, IEntity
  {
    [DisplayName("İzin Adı")]
    [Required(ErrorMessage = "{0} boş geçilemez")]
    [StringLength(80, ErrorMessage = "{0}, {1}'den az olmalıdır.")]
    [MinLength(2, ErrorMessage = "{0}, {1}'den fazla olmalıdır")]
    public string Name { get; set; }

    public virtual ICollection<AuthorityPermission>? AuthorityPermission { get; set; }
  }
}
