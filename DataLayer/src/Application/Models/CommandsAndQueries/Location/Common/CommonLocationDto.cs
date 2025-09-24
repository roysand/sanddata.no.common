using DataLayer.Application.Common.Mappings;

namespace sanddata.no.ams.api.Application.Common.CommandsAndQueries.Location.Common;

public class CommonLocationDto : IMapFrom<DataLayer.Domain.Entities.Location>
{
    public bool IsActive { get; set; }
    public string LocationName { get; set; } = null!;
    public string? LocationAddress { get; set; }
    public string? SerialNumber { get; set; }
    
    public void Mapping(AutoMapper.Profile profile)
    {
        profile.CreateMap<CommonLocationDto, DataLayer.Domain.Entities.Location>().ReverseMap();
    }
}