using AutoMapper;
using DataLayer.Application.Common.Mappings;
using sanddata.no.ams.api.Application.Common.CommandsAndQueries.Location.Common;

namespace DataLayer.Application.Models.CommandsAndQueries.Location.Create;

public class CreateLocationDto : CommonLocationDto, IMapFrom<Domain.Entities.Location>
{
    public new void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Entities.Location, CommonLocationDto>().ReverseMap();
    }
}