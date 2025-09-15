namespace DataLayer.Domain.Entities;

public class AppUserLocation
{
    public int AppUserId { get; set; }
    public AppUser AppUser { get; set; } = null!;

    public int LocationId { get; set; }
    public Location Location { get; set; } = null!;
}