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
using TesteTécnicoIdeal.API.Services;
using TesteTécnicoIdeal.UnitTests.DependenciesBuilder;

namespace TesteTécnicoIdeal.UnitTests.Tests.UserControllerTests
{
    public class AddUserTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly UserController _userController;
        public AddUserTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _userController = new UserController(_userServiceMock.Object);
        }

        [Fact]
        public async Task Returns_Ok()
        {
            var userDTO = new UserDTO { Name = "Test Name", Surname = "TestSurname", PhoneNumber = "99999999"};
            _userServiceMock.Setup(service => service.AddUser(userDTO)).Returns(Task.CompletedTask);

            var result = await _userController.Post(userDTO);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Returns_StatusCode400()
        {
            var invalidUserDTO = new UserDTO { Name = "", PhoneNumber = "", Surname = ""};
            CustomExceptions customError = new CustomExceptions("expected error message");
            _userServiceMock.Setup(service => service.AddUser(invalidUserDTO)).ThrowsAsync(customError);

            var result = await _userController.Post(invalidUserDTO);

            var BadRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(customError.ErrorMessages, BadRequestResult.Value);

        }

        [Fact]
        public async Task Returns_StatusCode500()
        {
            string unexpectedErrorMessage = "Um erro inesperado aconteceu(API).";
            var UserDTO = new UserDTO ();
            _userServiceMock.Setup(service => service.AddUser(UserDTO)).ThrowsAsync(new SystemException());

            var result = await _userController.Post(UserDTO);

            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal(unexpectedErrorMessage, statusCodeResult.Value);
        }

    }
}
