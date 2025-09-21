namespace DataLayer.Domain.Entities;

public class AppUser
{
    public Guid AppUserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string HashedPassword { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
    public virtual ICollection<AppUserLocation>? AppUserLocations { get; init; } = new List<AppUserLocation>();
    public virtual ICollection<AppUserRole>? AppUserRoles  { get; init; } = new List<AppUserRole>();

    public AppUser()
    {
    }

    public AppUser(Guid appUserId, string firstName, string lastName, string hashedPassword, string email)
    {
        AppUserId = appUserId;
        FirstName = firstName;
        LastName = lastName;
        HashedPassword = hashedPassword;
        Email = email;
    }
}