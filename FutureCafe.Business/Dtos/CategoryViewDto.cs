using System.ComponentModel;

namespace FutureCafe.Business.Dtos
{
  public class CategoryViewDto
  {
    public int Id { get; set; }

    [DisplayName("Kategori Adı")]
    public string Name { get; set; }
  }
}
