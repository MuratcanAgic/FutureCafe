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
      CreateMap<SchoolClass, SchoolClassViewDto>().ReverseMap();

      CreateMap<Category, CategoryCreateEditDto>().ReverseMap();
      CreateMap<Category, CategoryViewDto>().ReverseMap();

      CreateMap<Student, StudentViewDto>().ReverseMap();
      CreateMap<Student, StudentCreateEditDto>().ReverseMap();
    }
  }
}
