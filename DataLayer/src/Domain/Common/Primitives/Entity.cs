namespace DataLayer.Domain.Common.Primitives;
public abstract class Entity<TKey> : IEquatable<Entity<TKey>>
{
    public TKey Id { get; private set; }

    protected Entity(TKey id)
    {
        Id = id;
    }
    
    public static bool operator ==(Entity<TKey>? first, Entity<TKey>? second)
    {
        return first is not null && second is not null && first.Equals(second);
    }

    public static bool operator !=(Entity<TKey>? first, Entity<TKey>? second)
    {
        return !(first == second);
    }

    public override bool Equals(object? obj)
    {
        return obj is Entity<TKey> other && Equals(other);
    }

    public bool Equals(Entity<TKey>? other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (GetType() != other.GetType())
            return false;

        if (Id is null || other.Id is null)
            return false;

        return Id.Equals(other.Id);
    }

    public override int GetHashCode()
    {
        return Id?.GetHashCode() * 41 ?? 0;
    }
}

