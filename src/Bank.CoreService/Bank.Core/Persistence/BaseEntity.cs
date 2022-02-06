namespace Bank.Core.Persistence
{
    public abstract class BaseEntity<TId> where TId : notnull
    {
        public TId Id { get; set; }
    }
}
