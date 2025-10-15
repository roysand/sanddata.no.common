using DataLayer.Domain.Common.Entities;

namespace DataLayer.Domain.Entities;

public sealed class Role : AuditableEntity
{
    public Role(Guid id, string roleName, string roleDescription) : base(id)
    {
        RoleName = roleName;
        RoleDescription = roleDescription;
    }

    public Guid RoleId { get; set; }
    public string RoleName { get; set; } = null!;
    public string RoleDescription { get; set; } = null!;
    public ICollection<AppUserRole> AppUserRoles{ get; init; } = new List<AppUserRole>();
}