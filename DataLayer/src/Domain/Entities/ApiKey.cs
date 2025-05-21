using DataLayer.Domain.Common.Entities;

namespace DataLayer.Domain.Entities;

public class ApiKey : AuditableEntity
{
    public Guid ApiKeyId { get; set; }
    public string UserId { get; set; } = null!;
    public string Key { get; set; } = string.Empty;
    public bool Admin { get; set; }
    public User User { get; set; } = null!;
}