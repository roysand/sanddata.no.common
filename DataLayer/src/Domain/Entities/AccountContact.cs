using DataLayer.Domain.Common.Entities;

namespace DataLayer.Domain.Entities;

public sealed class AccountContact : AuditableEntity
{
    public AccountContact(Guid id, Guid accountId, string contactFirstName, string contactLastName, string contactEmail, string? contactMobilePhone) : base(id)
    {
        AccountId = accountId;
        ContactFirstName = contactFirstName;
        ContactLastName = contactLastName;
        ContactEmail = contactEmail;
        ContactMobilePhone = contactMobilePhone;
    }

    public Guid AccountId { get; set; }

    public string ContactFirstName { get; set; } = null!;
    public string ContactLastName { get; set; } = null!;
    public string ContactEmail { get; set; } = null!;
    public string? ContactMobilePhone { get; set; }
    public Account Account { get; set; } = null!;
}
