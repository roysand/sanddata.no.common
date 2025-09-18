﻿namespace DataLayer.Domain.Entities;

public class AppUserRole
{
    public Guid AppUserId { get; set; }
    public AppUser AppUser { get; set; } = null!;
    
    public Guid RoleId { get; set; }
    public Role Role { get; set; } = null!;
}