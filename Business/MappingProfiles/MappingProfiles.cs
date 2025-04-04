using AutoMapper;
using DTO.Garage;
using DTO.Otel;
using DTO.Person;
using Entities;

namespace Business.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //REVERSE MAP CREATE İŞLEMİ İÇİN KULLANILIYOR ÇİFT TARAFLI İŞLEMLER İÇİN REVERSMAP YAPILIR ÇOK ÖNEMLİ UNUTMA!!!!!!!!!!!!!!!!
            // Otel Mapping
            CreateMap<Otel, OtelDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.OtelName))
                .ReverseMap();

            // Garage Mapping
            CreateMap<Garage, GarageDto>()
                .ForMember(dest => dest.OtelName, opt => opt.MapFrom(src => src.otel.OtelName))
                .ReverseMap();

            CreateMap<GarageDto, Garage>()
                .ForMember(dest => dest.otel, opt => opt.Ignore());

            CreateMap<UserRegisterDto, Person>()
    .ForMember(dest => dest.Role, opt => opt.MapFrom(src => UserRole.User))
    .ReverseMap();

            CreateMap<UserLoginDto, Person>().ReverseMap();

            CreateMap<Person, UserResponseDto>();

            CreateMap<Person, UserLoginDto>().ReverseMap();



        }
    }
}
