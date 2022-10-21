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

    public bool IsTransient()
    {
        // after change Id from int to Guid, this method was deprecated
        return true;// this.Id == default(Guid);
    }

    public override bool Equals(object obj)
    {

        if (obj == null || !(obj is Entity<TKey>))
            return false;

        if (Object.ReferenceEquals(this, obj))
            return true;

        if (this.GetType() != obj.GetType())
            return false;

        Entity<TKey> item = (Entity<TKey>)obj;

        if (item.IsTransient() || this.IsTransient())
            return false;
        else
            return item.Id.Equals(this.Id);
    }

    public override int GetHashCode()
    {
        if (!IsTransient())
        {
            if (!_requestedHashCode.HasValue)
                _requestedHashCode = this.Id.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)

            return _requestedHashCode.Value;
        }
        else
            return base.GetHashCode();

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
