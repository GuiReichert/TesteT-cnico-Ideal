using Microsoft.AspNetCore.Mvc;
using TesteTécnicoIdeal.API.DTO_s;
using TesteTécnicoIdeal.API.Models;
using TesteTécnicoIdeal.API.Services;

namespace TesteTécnicoIdeal.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();

            if(users != null)
            {
                return Ok(users);
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);

            if(user != null)
            {
                return Ok(user);
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult> Post(AddUserDTO request)
        {
            try
            {
                await _userService.AddUser(request);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        public async Task<ActionResult> Put(User request)
        {
            try
            {
                await _userService.UpdateUser(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _userService.DeleteUser(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        // A FAZER : Tratar Exceções e adicionar mensagem de "Erro desconhecido" para erros não tratados, não deixando o programa jogar a mensagem de erro para o usuário
    }
}
