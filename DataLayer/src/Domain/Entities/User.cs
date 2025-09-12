using Microsoft.AspNetCore.Identity;

namespace DataLayer.Domain.Entities;

public sealed class User : IdentityUser
{
    public Guid? LocationId { get; set; }
    public Location? Location { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}