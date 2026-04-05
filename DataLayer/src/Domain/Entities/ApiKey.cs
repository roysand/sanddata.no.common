using DataLayer.Domain.Common.Entities;

namespace DataLayer.Domain.Entities;

public sealed class ApiKey : AuditableEntity
{
    public ApiKey(Guid id, string key, bool admin) : base(id)
    {
        Key = key;
        Admin = admin;
    }

    public string Key { get; set; } = null!;
    public bool Admin { get; set; }
    public ICollection<Account> Account { get; set; } = new List<Account>();
    public ICollection<Location> Location { get; set; } = new List<Location>();
}
