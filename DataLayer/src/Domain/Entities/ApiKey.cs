using DataLayer.Domain.Common.Entities;

namespace DataLayer.Domain.Entities;

public class ApiKey : AuditableEntity
{
    public Guid ApiKeyId { get; set; }

    public string Key { get; set; } = null!;
    
    public virtual ICollection<Account> Account { get; set; } = new List<Account>();
}