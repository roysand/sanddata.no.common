using DataLayer.Application.Common.Mappings;
using NetTopologySuite.Geometries;

namespace DataLayer.Application.Models.CommandsAndQueries.AppUser.Common;

public class CommonAppUserVm : IMapFrom<DataLayer.Domain.Entities.AppUser>
{
    public Guid AppUserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public ICollection<Domain.Entities.Location>? Locations { get; set; }

    public CommonAppUserVm()
    {
        
    }
    public CommonAppUserVm(string firstName, string lastName, string email, ICollection<Domain.Entities.Location> locations)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Locations = locations;
    }
}