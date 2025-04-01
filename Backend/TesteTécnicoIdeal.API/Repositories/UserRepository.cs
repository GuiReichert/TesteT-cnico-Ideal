using Microsoft.EntityFrameworkCore;
using TesteTécnicoIdeal.API.Database;
using TesteTécnicoIdeal.API.Exceptions;
using TesteTécnicoIdeal.API.Models;

namespace TesteTécnicoIdeal.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TesteTécnicoIdeal_DbContext _dbContext;

        public UserRepository(TesteTécnicoIdeal_DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = await _dbContext.Users.AsNoTracking().ToListAsync();

            return users;
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                throw new CustomExceptions("Este cadastro não existe.");
            }

            return user;
        }

        public async Task AddUser(User request)
        {
            await _dbContext.Users.AddAsync(request);
        }

        public async Task UpdateUser(User request)
        {
            var userToUpdate = await _dbContext.Users.FindAsync(request.Id);

            if (userToUpdate == null)
            {
                throw new CustomExceptions("Este cadastro não existe.");
            }


            userToUpdate.Name = request.Name;
            userToUpdate.Surname = request.Surname;
            userToUpdate.PhoneNumber = request.PhoneNumber;
        }

        public async Task DeleteUser(int id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                throw new CustomExceptions("Este cadastro não existe.");
            }
            _dbContext.Remove(user);
        }
    }
}
