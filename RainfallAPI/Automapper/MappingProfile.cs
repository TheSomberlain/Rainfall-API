using AutoMapper;
using RainfallAPI.Models.InputModels;
using RainfallAPI.Models.ViewModels;

namespace RainfallAPI.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ReadingItem, RainfallReading>()
                .ForMember(dest => dest.DateMeasured, opt => opt.MapFrom(src => src.DateTime))
                .ForMember(dest => dest.AmountMeasured, opt => opt.MapFrom(src => src.Value));
        }
    }
}
