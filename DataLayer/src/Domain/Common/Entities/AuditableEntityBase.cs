namespace DataLayer.Domain.Common.Entities;

public abstract class AuditableEntityBase
{
    public DateTime Modified { get; set; }
}