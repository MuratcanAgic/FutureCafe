using System.ComponentModel;

namespace FutureCafe.Business.Dtos
{
  public class UserForLoginDto
  {
    [DisplayName("Kullanıcı Adı")]
    public string Email { get; set; }
    [DisplayName("Şifre")]
    public string Password { get; set; }
  }
}
