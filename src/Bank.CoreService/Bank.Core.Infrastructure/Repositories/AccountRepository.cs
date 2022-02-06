using Bank.Core.Aggregates;
using Bank.Core.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Bank.Core.Infrastructure.Repositories
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
