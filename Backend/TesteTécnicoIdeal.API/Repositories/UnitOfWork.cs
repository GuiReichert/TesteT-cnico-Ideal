using TesteTécnicoIdeal.API.Database;

namespace TesteTécnicoIdeal.API.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TesteTécnicoIdeal_DbContext _dbContext;

        public UnitOfWork(TesteTécnicoIdeal_DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CommitChanges()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
