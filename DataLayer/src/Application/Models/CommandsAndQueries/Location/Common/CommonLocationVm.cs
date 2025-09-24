using DataLayer.Application.Common.Mappings;

namespace DataLayer.Application.Models.CommandsAndQueries.Location.Common;

public class CommonLocationVm : IMapFrom<DataLayer.Domain.Entities.Location>
{
    public bool IsActive { get; set; }
    public string LocationName { get; set; } = null!;
    public string? LocationAddress { get; set; }
    public string? SerialNumber { get; set; }
    
    public void Mapping(AutoMapper.Profile profile)
    {
        profile.CreateMap<DataLayer.Domain.Entities.Location, CommonLocationVm>().ReverseMap();
    }
}