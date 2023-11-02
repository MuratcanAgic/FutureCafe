using FutureCafe.Core.Entitites;
using FutureCafe.Entities.Abstract;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FutureCafe.Entities.Concrete
{
  public class Credit : BaseEntity, IEntity
  {
    [DataType(DataType.Currency)]
    [DisplayFormat(DataFormatString = "{0:c}")]
    //[Required(ErrorMessage = "{0} boş geçilemez")]
    [DisplayName("Miktar")]
    public decimal Amount { get; set; }

    public virtual ICollection<StudentCredit> StudentCredit { get; set; }
  }
}
