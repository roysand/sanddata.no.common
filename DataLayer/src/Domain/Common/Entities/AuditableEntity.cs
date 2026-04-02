namespace DataLayer.Domain.Common.Entities;

public abstract class AuditableEntity : AuditableEntityBase
{
    protected AuditableEntity(Guid id) : base(id)
    {
    }

    public DateTime? CreatedDate { get; set; }
}