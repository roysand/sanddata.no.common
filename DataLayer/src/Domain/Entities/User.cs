using DataLayer.Domain.Common.Entities;

namespace DataLayer.Domain.Entities;

public class User : AuditableEntity
{
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public bool IsActive { get; set; } = false;

    public virtual ApiKey? ApiKey { get; set; }
    public virtual ICollection<Location> Locations { get; set; }

    public User()
    {
        Locations = new List<Location>();
    }
}