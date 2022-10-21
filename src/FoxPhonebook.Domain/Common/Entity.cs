namespace FoxPhonebook.Domain.Common;
public abstract class Entity<TKey>
{
    int? _requestedHashCode;

    TKey _Id;

    public virtual TKey Id
    {
        get
        {
            return _Id;
        }
        protected set
        {
            _Id = value;
        }
    }

    public override bool Equals(object obj)
    {
        if (obj == null || !(obj is Entity<TKey>))
            return false;

        if (Object.ReferenceEquals(this, obj))
            return true;

        if (this.GetType() != obj.GetType())
            return false;

        var item = obj as Entity<TKey>;

        if (item is null)
            return false;

        if (item.Id is null)
            return false;

        return item.Id.Equals(this.Id);
    }

    public static bool operator ==(Entity<TKey> left, Entity<TKey> right)
    {
        if (Object.Equals(left, null))
            return (Object.Equals(right, null)) ? true : false;
        else
            return left.Equals(right);
    }

    public static bool operator !=(Entity<TKey> left, Entity<TKey> right)
    {
        return !(left == right);
    }
}
