using Bank.Core.Aggregates;
using Bank.Core.Persistence;
using Bank.Core.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Bank.Core.Infrastructure.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly BankContext _dbContext;
        public CustomerRepository(DbContext dbContext) : base(dbContext)
        {
            _dbContext = (BankContext?)dbContext?? throw new ArgumentNullException("missing registiration for dbcontext");
        }

        // todo: use specification
        public async Task<Customer?> GetWithAllAccountsByIdAsync(Guid id)
        {
            return await _dbContext.Customer.Where(r => r.Id == id)
                    .Include(r => r.Accounts).FirstOrDefaultAsync();
        }
    }
}
