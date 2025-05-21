using DataLayer.Domain.Common.Entities;

namespace DataLayer.Domain.Entities;

public class Location : AuditableEntity
{
    public Guid LocationId { get; set; }

    public string LocationName { get; set; } = null!;

    public string? LocationAddress { get; set; }
    public string Zone { get; set; } = string.Empty;

    public string? SerialNumber { get; set; }

    public ICollection<User> Users { get; set; }

    public Location()
    {
        Users = new List<User>();
    }
}