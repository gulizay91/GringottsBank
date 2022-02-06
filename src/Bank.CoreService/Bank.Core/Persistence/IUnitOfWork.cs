namespace Bank.Core.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountRepository AccountRepository { get; }
        ICustomerRepository CustomerRepository { get; }

        Task<int> SaveChangesAsync();
        int SaveChanges();
    }
}
