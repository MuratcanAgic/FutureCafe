using FutureCafe.Entities.Concrete;
using System.ComponentModel;

namespace FutureCafe.Business.Dtos
{
  public class UserForViewDto
  {
    public int Id { get; set; }
    [DisplayName("Kullanıcı Adı")]
    public string Email { get; set; }
    [DisplayName("İsim")]
    public string FirstName { get; set; }
    [DisplayName("Soyisim")]
    public string LastName { get; set; }

    [DisplayName("Roller")]
    public ICollection<UserOperationClaim> UserOperationClaims { get; set; }
  }
}
