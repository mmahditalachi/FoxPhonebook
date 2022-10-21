namespace FoxPhonebook.Domain.Common;

public interface IAuditableEntity
{
    DateTime Created { get; set; }

    Guid? CreatedBy { get; set; }

    DateTime? LastModified { get; set; }

    Guid? LastModifiedBy { get; set; }
}

public abstract class AuditableEntity : IAuditableEntity
{
    public DateTime Created { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? LastModified { get; set; }

    public Guid? LastModifiedBy { get; set; }
}
