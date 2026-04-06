namespace DataLayer.Domain.Common.Entities;

/// <summary>
/// Base class for entities with composite primary keys that still need audit tracking.
/// Does not inherit from Entity&lt;TKey&gt; since there is no single surrogate key.
/// </summary>
public abstract class AuditableCompositeEntity
{
    public DateTime ChangedDate { get; set; }
    public DateTime? CreatedDate { get; set; }
}
