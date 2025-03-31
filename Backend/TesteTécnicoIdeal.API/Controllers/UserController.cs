
using Microsoft.AspNetCore.Mvc;
using TesteTécnicoIdeal.API.DTO_s;
using TesteTécnicoIdeal.API.Exceptions;
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
            try
            {
                var users = await _userService.GetAllUsers();

                return Ok(users);
            }
            catch (Exception ex)
            {
                if (ex is CustomExceptions)
                {
                    var exception = ex as CustomExceptions;

                    return BadRequest(exception.ErrorMessages);
                }
                else return StatusCode(500, "Um erro inesperado aconteceu(API).");
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            try
            {
                var user = await _userService.GetUserById(id);

                return Ok(user);
            }
            catch(Exception ex)
            {
                if (ex is CustomExceptions) 
                {
                    var exception = ex as CustomExceptions;

                    return BadRequest(exception.ErrorMessages);
                }
                else return StatusCode(500, "Um erro inesperado aconteceu(API).");

            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(UserDTO request)
        {
            try
            {
                await _userService.AddUser(request);
                return Ok();
            }
            catch(Exception ex)
            {
                if (ex is CustomExceptions)
                {
                    var exception = ex as CustomExceptions;

                    return BadRequest(exception.ErrorMessages);
                }
                else return StatusCode(500, "Um erro inesperado aconteceu(API).");
            }

        }

        [HttpPut]
        public async Task<ActionResult> Put(UserDTO request, int id)
        {
            try
            {
                await _userService.UpdateUser(request , id);
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex is CustomExceptions)
                {
                    var exception = ex as CustomExceptions;

                    return BadRequest(exception.ErrorMessages);
                }
                else return StatusCode(500, "Um erro inesperado aconteceu(API).");
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
                if (ex is CustomExceptions)
                {
                    var exception = ex as CustomExceptions;

                    return BadRequest(exception.ErrorMessages);
                }
                else return StatusCode(500, "Um erro inesperado aconteceu(API).");
            }
        }

    }
}
