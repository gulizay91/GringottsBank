using Bank.Core.Exceptions;
using Bank.Core.Persistence;
using Bank.Core.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Bank.Core.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly BankContext _dbContext;

        private readonly IAccountRepository _accountRepository;

        private readonly ICustomerRepository _customerRepository;

        private bool _disposed = false;

        private Dictionary<Type, object> _repositories;


        public UnitOfWork(BankContext context)
        {
            _dbContext = context;
        }



        public IAccountRepository AccountRepository => _accountRepository ?? new AccountRepository(_dbContext);

        public ICustomerRepository CustomerRepository => _customerRepository ?? new CustomerRepository(_dbContext);


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<Type, object>();
            }

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = new Repository<TEntity>(_dbContext);
            }

            return (IRepository<TEntity>)_repositories[type];
        }

        public Task<int> SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            try
            {
                return _dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)//todo
            {
                throw new UoFUpdateConcurrencyException();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this._disposed = true;
        }

    }
}
