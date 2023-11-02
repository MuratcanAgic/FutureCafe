using FutureCafe.Core.Entitites;
using FutureCafe.Entities.Abstract;
using System.ComponentModel;

namespace FutureCafe.Entities.Concrete
{
  public class Category : BaseEntity, IEntity
  {
    [DisplayName("Kategori Adı")]
    //[Required(ErrorMessage = "{0} boş geçilemez")]
    //[StringLength(80, ErrorMessage = "{0}, {1}'den az olmalıdır.")]
    //[MinLength(2, ErrorMessage = "{0}, {1}'den fazla olmalıdır")]
    public string Name { get; set; }

    public virtual ICollection<StudentCategory>? StudentCategory { get; set; }
    public virtual ICollection<ProductCategory>? ProductCategory { get; set; }
  }
}
