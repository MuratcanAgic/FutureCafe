using FutureCafe.Core.Entitites;
using FutureCafe.Entities.Abstract;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FutureCafe.Entities.Concrete
{
  public class Authority : BaseEntity, IEntity
  {
    [DisplayName("Yetkinlik Adı")]
    [Required(ErrorMessage = "{0} boş geçilemez")]
    [StringLength(80, ErrorMessage = "{0}, {1}'den az olmalıdır.")]
    [MinLength(2, ErrorMessage = "{0}, {1}'den fazla olmalıdır")]
    public string Name { get; set; }

    [DisplayName("Yetkinlik Tanımı")]
    [Required(ErrorMessage = "{0} boş geçilemez")]
    [StringLength(150, ErrorMessage = "{0}, {1}'den az olmalıdır.")]
    [MinLength(2, ErrorMessage = "{0}, {1}'den fazla olmalıdır")]
    public string Description { get; set; }

    public virtual ICollection<AuthorityPermission>? AuthorityPermission { get; set; }
  }
}
