using DataLayer.Domain.Common.Entities;

namespace DataLayer.Domain.Entities;

public sealed class AppUser : AuditableEntity
{
    public bool IsActive { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string HashedPassword { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
    public ICollection<AppUserLocation>? AppUserLocations { get; init; } = new List<AppUserLocation>();
    public ICollection<AppUserRole>? AppUserRoles { get; init; } = new List<AppUserRole>();

    public AppUser(Guid id, bool isActive, string? refreshToken, DateTime? refreshTokenExpiryTime) : base(id)
    {
        IsActive = isActive;
        RefreshToken = refreshToken;
        RefreshTokenExpiryTime = refreshTokenExpiryTime;
    }

    public AppUser(Guid id, string firstName, string lastName, string hashedPassword, string email)
        : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        HashedPassword = hashedPassword;
        Email = email;
    }
}
