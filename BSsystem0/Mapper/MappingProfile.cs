using AutoMapper;
using BSsystem0.Models;

namespace BSsystem.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Appointment, AppointmentDTO>();

            CreateMap<AppointmentDTO, Appointment>();

            CreateMap<User, UserRegister>().ReverseMap();

            CreateMap<UserRegister, User>();

            CreateMap<User, UserLogin>();

            CreateMap<UserLogin, User>();

            CreateMap<Service, ServiceAdder>();

            CreateMap<ServiceAdder, Service>();
        }
    }
}
