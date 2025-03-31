using TesteTécnicoIdeal.API.Models;

namespace TesteTécnicoIdeal.API.Repositories
{
    public interface IUserRepository
    {
        public Task<List<User>> GetAllUsers();
        public Task<User> GetUserById(int id);
        public Task AddUser(User request);
        public Task UpdateUser(User request);
        public Task DeleteUser(int id);



    }
}
