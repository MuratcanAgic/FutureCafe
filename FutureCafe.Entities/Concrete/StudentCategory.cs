﻿using FutureCafe.Core.Entitites;
using FutureCafe.Entities.Abstract;

namespace FutureCafe.Entities.Concrete
{
  public class StudentCategory : BaseEntity, IEntity
  {
    public int StudentId { get; set; }
    public int CategoryId { get; set; }

    public Student Student { get; set; }
    public Category Category { get; set; }
  }
}
