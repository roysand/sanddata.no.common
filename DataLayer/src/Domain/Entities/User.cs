using Microsoft.AspNetCore.Identity;

namespace DataLayer.Domain.Entities;

public sealed class User : IdentityUser
{
    public Location? Location { get; set; }
}