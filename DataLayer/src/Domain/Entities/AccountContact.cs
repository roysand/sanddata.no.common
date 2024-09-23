using DataLayer.Domain.Common.Entities;

namespace DataLayer.Domain.Entities;

public class AccountContact : AuditableEntity
{
    public Guid AccountContactId { get; set; }
    public Guid AccountId { get; set; }
    public string ContactFirstName { get; set; } = null!;
    public string ContactLastName { get; set; } = null!;
    public string ContactEmail { get; set; } = null!;
    public string? ContactMobilePhone { get; set; }
    public virtual Account Account { get; set; } = null!;
}