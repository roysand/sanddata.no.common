using DataLayer.Domain.Common.Entities;

namespace DataLayer.Domain.Entities;

public class AppUserLocation : AuditableCompositeEntity
{
    public Guid AppUserId { get; set; }
    public AppUser AppUser { get; set; } = null!;

    public Guid LocationId { get; set; }
    public Location Location { get; set; } = null!;
}
