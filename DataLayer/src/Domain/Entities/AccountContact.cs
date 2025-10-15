using DataLayer.Domain.Common.Entities;

namespace DataLayer.Domain.Entities;

public sealed class AccountContact : AuditableEntity
{
    public AccountContact(Guid id, string contactFirstName, string contactLastName, string contactEmail, string? contactMobilePhone) : base(id)
    {
        ContactFirstName = contactFirstName;
        ContactLastName = contactLastName;
        ContactEmail = contactEmail;
        ContactMobilePhone = contactMobilePhone;
    }

    public Guid AccountContactId { get; set; }
    public Guid AccountId { get; set; }
    
    public string ContactFirstName { get; set; } = null!;
    public string ContactLastName { get; set; } = null!;
    public string ContactEmail { get; set; } = null!;
    public string? ContactMobilePhone { get; set; }
    public Account Account { get; set; } = null!;
}