namespace DataLayer.Domain.Entities;

public class UserLocation
{
    public string UserId { get; set; } = string.Empty;
    public User User { get; set; } = null!;

    public string LocationId { get; set; } = string.Empty;
    public Location Location { get; set; } = null!;
}