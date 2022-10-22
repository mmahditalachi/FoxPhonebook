using System.ComponentModel.DataAnnotations.Schema;

namespace FoxPhonebook.Domain.Common
{
    public interface IAggregateRoot
    {

    }

    public abstract class AggregateRoot<TKey> :
        AuditableEntity, IAggregateRoot, IHasDomainEvent
    {
        int? _requestedHashCode;

        TKey _id;

        public virtual TKey Id
        {
            get
            {
                return _id;
            }
            protected set
            {
                _id = value;
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is AggregateRoot<TKey>))
                return false;

            if (Object.ReferenceEquals(this, obj))
                return true;

            if (this.GetType() != obj.GetType())
                return false;

            var item = obj as AggregateRoot<TKey>;

            if (item == null || item.Id == null)
                return false;

            return item.Id.Equals(this.Id);
        }

        [NotMapped]
        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();


        public static bool operator ==(AggregateRoot<TKey> left, AggregateRoot<TKey> right)
        {
            if (Object.Equals(left, null))
                return (Object.Equals(right, null)) ? true : false;
            else
                return left.Equals(right);
        }

        public static bool operator !=(AggregateRoot<TKey> left, AggregateRoot<TKey> right)
        {
            return !(left == right);
        }
    }

}
