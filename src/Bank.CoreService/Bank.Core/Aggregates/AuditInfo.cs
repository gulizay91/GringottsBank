using Bank.Shared;

namespace Bank.Core.Aggregates
{
    public class AuditInfo : ValueObject
    {
        public Guid CreatedBy { get; private set; }
        public DateTime Created { get; private set; }

        public Guid? LastModifiedBy { get; private set; }
        public DateTime? LastModified { get; private set; }

        public static AuditInfo CreateNew(Guid createdBy, DateTime createdDate,
            Guid? modifiedBy = null, DateTime? modifiedDate = null)
        {
            return new AuditInfo
            {
                CreatedBy = createdBy,
                Created = createdDate,
                LastModifiedBy = modifiedBy,
                LastModified = modifiedDate
            };
        }

        public void RecordModifiedEvent(Guid modifiedBy, DateTime? modifiedDate = null)
        {
            LastModified = modifiedDate ?? DateTime.UtcNow;
            LastModifiedBy = modifiedBy;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return new[] { CreatedBy, LastModifiedBy };
        }
    }
}
