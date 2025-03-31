using TesteTécnicoIdeal.API.DTO_s;
using TesteTécnicoIdeal.API.Models;

namespace TesteTécnicoIdeal.API.Services
{
    public interface IUserService
    {
        public Task<List<User>> GetAllUsers();
        public Task<User> GetUserById(int id);
        public Task AddUser(AddUserDTO request);
        public Task UpdateUser(User request);
        public Task DeleteUser(int id);
    }
}
