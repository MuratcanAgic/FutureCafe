using AutoMapper;
using FutureCafe.Business.Dtos;
using FutureCafe.Entities.Concrete;

namespace FutureCafe.Business.Mapping
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<SchoolClass, SchoolClassCreateEditDto>().ReverseMap();
      CreateMap<Category, CategoryCreateEditDto>().ReverseMap();
      CreateMap<Student, StudentViewDto>().ReverseMap();
      CreateMap<Student, StudentCreateEditDto>().ReverseMap();
    }
  }
}
