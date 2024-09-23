namespace DataLayer.Domain.Common.Entities;

public abstract class AuditableEntity : AuditableEntityBase
{
    public DateTime? Created { get; set; }
}