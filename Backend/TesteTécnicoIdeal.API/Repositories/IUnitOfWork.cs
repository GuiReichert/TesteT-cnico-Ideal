namespace TesteTécnicoIdeal.API.Repositories
{
    public interface IUnitOfWork
    {
        public Task CommitChanges();
    }
}
