using FutureCafe.Core.Entitites;
using FutureCafe.Entities.Abstract;
using System.ComponentModel;

namespace FutureCafe.Entities.Concrete
{
  public class SchoolClass : BaseEntity, IEntity
  {
    [DisplayName("Sınıf Adı")]
    //[Required(ErrorMessage = "{0} boş geçilemez")]
    //[StringLength(100, ErrorMessage = "{0}, {1}'den az olmalıdır.")]
    //[MinLength(2, ErrorMessage = "{0}, {1}'den fazla olmalıdır")]
    public string Name { get; set; }
  }
}
