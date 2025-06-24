using AutoMapper;
using DesktopService.Dtos;
using DesktopService.Entity;

namespace DesktopService.RequestHelpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<ForeignSentence, ForeignSentenceDto>();
        CreateMap<ForeignSentenceDto, ForeignSentence>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}
