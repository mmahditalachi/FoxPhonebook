using System.ComponentModel.DataAnnotations.Schema;

namespace FoxPhonebook.Domain.Common
{
    public interface IAggregateRoot
    {

    }

    public abstract class AggregateRoot<TKey> :
        AuditableEntity, IAggregateRoot, IHasDomainEvent
    {
        public virtual TKey Id { get; protected set; }

        [NotMapped]
        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }

}
