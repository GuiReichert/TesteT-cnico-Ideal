using AutoMapper;
using TesteTécnicoIdeal.API.DTO_s;
using TesteTécnicoIdeal.API.Models;
using TesteTécnicoIdeal.API.Repositories;

namespace TesteTécnicoIdeal.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _userRepository.GetUserById(id);
        }

        public async Task AddUser(UserDTO request)
        {
            var mappedRequest = _mapper.Map<User>(request);

            await _userRepository.AddUser(mappedRequest);
            await _unitOfWork.CommitChanges();
        }
        public async Task UpdateUser(UserDTO request, int id)
        {
            var mappedRequest = _mapper.Map<User>(request);
            mappedRequest.Id = id;

            await _userRepository.UpdateUser(mappedRequest);
            await _unitOfWork.CommitChanges();
        }

        public async Task DeleteUser(int id)
        {
            await _userRepository.DeleteUser(id);
            await _unitOfWork.CommitChanges();
        }

    }
}
