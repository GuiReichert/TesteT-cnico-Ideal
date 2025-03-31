using TesteTécnicoIdeal.API.DTO_s;
using TesteTécnicoIdeal.API.Models;

namespace TesteTécnicoIdeal.API.Services
{
    public interface IUserService
    {
        public Task<List<User>> GetAllUsers();
        public Task<User> GetUserById(int id);
        public Task AddUser(UserDTO request);
        public Task UpdateUser(UserDTO request, int id);
        public Task DeleteUser(int id);
    }
}
