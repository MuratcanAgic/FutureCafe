using FutureCafe.Core.Entitites;
using FutureCafe.Entities.Abstract;
using System.ComponentModel;

namespace FutureCafe.Entities.Concrete
{
  public class Trade : BaseEntity, IEntity
  {
    [DisplayName("Öğrenci")]
    //[Required(ErrorMessage = "{0} boş geçilemez")]
    public int StudentId { get; set; }

    public Student Student { get; set; }

    public virtual ICollection<TradeProduct> TradeProduct { get; set; }
  }
}
