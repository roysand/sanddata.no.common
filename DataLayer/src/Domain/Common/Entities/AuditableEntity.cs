namespace DataLayer.Domain.Common.Entities;

public abstract class AuditableEntity : AuditableEntityBase
{
    public DateTime? CreatedDate { get; set; }
}