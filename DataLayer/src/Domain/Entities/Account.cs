using DataLayer.Domain.Common.Entities;

namespace DataLayer.Domain.Entities;

public class Account : AuditableEntity
{
    public Guid AccountId { get; set; }
    public string AccountName { get; set; } = null!;
    public bool Active { get; set; }
    public Guid? ApiKeyId { get; set; }
    public virtual ICollection<AccountContact> AccountContact { get; set; } = new List<AccountContact>();
    public virtual ApiKey? ApiKey { get; set; }
}