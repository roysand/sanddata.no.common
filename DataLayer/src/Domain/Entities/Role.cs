using DataLayer.Domain.Common.Entities;

namespace DataLayer.Domain.Entities;

public class Role : AuditableEntity
{
    public Guid RoleId { get; set; } = Guid.NewGuid();
    public string RoleName { get; set; } = null!;
    public string RoleDescription { get; set; } = null!;
    public virtual ICollection<AppUserRole> AppUserRoles{ get; init; } = new List<AppUserRole>();
}