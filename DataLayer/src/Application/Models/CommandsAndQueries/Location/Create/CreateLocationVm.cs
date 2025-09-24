using AutoMapper;
using DataLayer.Application.Common.Mappings;
using DataLayer.Application.Models.CommandsAndQueries.Location.Common;

namespace DataLayer.Application.Models.CommandsAndQueries.Location.Create;

public class CreateLocationVm : CommonLocationVm, IMapFrom<DataLayer.Domain.Entities.Location>
{
    public Guid LocationId { get; set; }
    public new void Mapping(Profile profile)
    {
        profile.CreateMap<DataLayer.Domain.Entities.Location, CreateLocationVm>().ReverseMap();
    }
}