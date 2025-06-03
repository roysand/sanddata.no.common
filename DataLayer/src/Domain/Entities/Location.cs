using DataLayer.Domain.Common.Entities;

namespace DataLayer.Domain.Entities;

public class Location : AuditableEntity
{
    public Guid LocationId { get; set; }

    public string LocationName { get; set; } = null!;

    public string? LocationAddress { get; set; }

    public string? SerialNumber { get; set; }

    public Guid? ApiKeyId { get; set; }

    public virtual ApiKey? ApiKey { get; set; }
    public virtual ICollection<Detail> Details { get; set; } = new List<Detail>();
    public virtual ICollection<Hour> Hours { get; set; } = new List<Hour>();
    public virtual ICollection<Day> Days { get; set; } = new List<Day>();
}