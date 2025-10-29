using DataLayer.Domain.Common.Entities;
using DataLayer.Domain.Common.Primitives;

namespace DataLayer.Domain.Entities;

public sealed class Account : AuditableEntity
{
    private readonly List<AccountContact> _contacts = new();
    internal Account(Guid id, string accountName, bool active, Guid? apiKeyId) : base(id)
    {
        AccountName = accountName;
        Active = active;
        ApiKeyId = apiKeyId;
    }

    public Guid AccountId { get; set; }
    public string AccountName { get; set; } = null!;
    public bool Active { get; set; }
    public Guid? ApiKeyId { get; set; }
    public IReadOnlyCollection<AccountContact> AccountContact => _contacts;
    public ApiKey? ApiKey { get; set; }
}