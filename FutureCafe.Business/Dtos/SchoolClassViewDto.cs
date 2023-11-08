﻿using System.ComponentModel;

namespace FutureCafe.Business.Dtos
{
  public class SchoolClassViewDto
  {
    public int Id { get; set; }
    [DisplayName("Sınıf Adı")]
    public string Name { get; set; }
  }
}
