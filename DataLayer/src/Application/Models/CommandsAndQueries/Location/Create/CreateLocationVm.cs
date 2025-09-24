using AutoMapper;
using DataLayer.Application.Common.Mappings;
using sanddata.no.ams.api.Application.Common.CommandsAndQueries.Location.Common;

namespace DataLayer.Application.Models.CommandsAndQueries.Location.Create;

public class CreateLocationVm : CommonLocationVm, IMapFrom<Domain.Entities.AppUser>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateLocationVm, CommonLocationVm>().ReverseMap();
    }
}