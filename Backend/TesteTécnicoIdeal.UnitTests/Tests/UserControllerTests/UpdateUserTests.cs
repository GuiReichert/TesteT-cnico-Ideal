using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TesteTécnicoIdeal.API.Controllers;
using TesteTécnicoIdeal.API.DTO_s;
using TesteTécnicoIdeal.API.Exceptions;
using TesteTécnicoIdeal.API.Models;
using TesteTécnicoIdeal.API.Services;

namespace TesteTécnicoIdeal.UnitTests.Tests.UserControllerTests
{
    public class UpdateUserTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly UserController _userController;

        public UpdateUserTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _userController = new UserController(_userServiceMock.Object);
        }

        [Fact]
        public async Task Returns_Ok()
        {
            var userDTO = new UserDTO { Name = "Name Test", Surname = "Surname Test", PhoneNumber = "99999999" };
            var userId = 1;
            _userServiceMock.Setup(service => service.UpdateUser(userDTO, userId)).Returns(Task.CompletedTask);

            var result = await _userController.Put(userDTO, userId);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Returns_StatusCode400()
        {
            var invalidUserDTO = new UserDTO { Name = "", PhoneNumber = "", Surname = "" };
            int invalidUserId = 99999;
            CustomExceptions customError = new CustomExceptions("expected error message");
            _userServiceMock.Setup(service => service.UpdateUser(invalidUserDTO, invalidUserId)).ThrowsAsync(customError);

            var result = await _userController.Put(invalidUserDTO, invalidUserId);

            var BadRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(customError.ErrorMessages, BadRequestResult.Value);
        }

        [Fact]
        public async Task Returns_StatusCode500()
        {
            string unexpectedErrorMessage = "Um erro inesperado aconteceu(API).";
            var userDTO = new UserDTO();
            int userId = 1;
            _userServiceMock.Setup(service => service.UpdateUser(userDTO, userId)).ThrowsAsync(new SystemException());

            var result = await _userController.Put(userDTO, userId);

            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal(unexpectedErrorMessage, statusCodeResult.Value);
        }


    }
}