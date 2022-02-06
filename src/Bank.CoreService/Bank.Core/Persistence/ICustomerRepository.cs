using Bank.Core.Aggregates;

namespace Bank.Core.Persistence
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer?> GetWithAllAccountsByIdAsync(Guid id);
    }
}
