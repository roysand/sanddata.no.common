using DataLayer.Domain.Common.Entities;
using DataLayer.Domain.Common.Primitives;

namespace DataLayer.Domain.Entities;

public sealed class Account : Entity<Guid>
{
    public Account(Guid id, string accountName, bool active, Guid? apiKeyId, ApiKey? apiKey) : base(id)
    {
        AccountName = accountName;
        Active = active;
        ApiKeyId = apiKeyId;
        ApiKey = apiKey;
    }

    public Guid AccountId { get; set; }
    public string AccountName { get; set; } = null!;
    public bool Active { get; set; }
    public Guid? ApiKeyId { get; set; }
    public ICollection<AccountContact> AccountContact { get; set; } = new List<AccountContact>();
    public ApiKey? ApiKey { get; set; }
}