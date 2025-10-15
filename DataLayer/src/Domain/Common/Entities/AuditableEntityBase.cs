using DataLayer.Domain.Common.Primitives;

namespace DataLayer.Domain.Common.Entities;

public abstract class AuditableEntityBase : Entity<Guid>
{
    protected AuditableEntityBase(Guid id) : base(id)
    {
    }

    public DateTime ChangedDate { get; set; }
}