using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TesteTécnicoIdeal.API.Controllers;
using TesteTécnicoIdeal.API.Exceptions;
using TesteTécnicoIdeal.API.Models;
using TesteTécnicoIdeal.API.Services;

namespace TesteTécnicoIdeal.UnitTests.Tests.UserControllerTests
{
    public class GetAllUsersTest
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly UserController _userController;
        public GetAllUsersTest()
        {
            _userServiceMock = new Mock<IUserService>();
            _userController = new UserController(_userServiceMock.Object);
        }

        [Fact]
        public async Task Returns_Ok()
        {
            var usersList = new List<User> { new User { Id = 1, Name = "Test Name", Surname = "Test Surname", PhoneNumber = "99999999" } };
            _userServiceMock.Setup(service => service.GetAllUsers()).ReturnsAsync(usersList);

            var result = await _userController.GetAllUsers();

            var OkObjectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(usersList, OkObjectResult.Value);
        }


        [Fact]
        public async Task Returns_StatusCode400()
        {
            CustomExceptions customError = new CustomExceptions("erro esperado");
            _userServiceMock.Setup(service => service.GetAllUsers()).ThrowsAsync(customError);

            var result = await _userController.GetAllUsers();

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(customError.ErrorMessages, badRequestResult.Value);
        }


        [Fact]
        public async Task Returns_StatusCode500()
        {
            string unexpectedErrorMessage = "Um erro inesperado aconteceu(API).";
            _userServiceMock.Setup(service => service.GetAllUsers()).ThrowsAsync(new SystemException());

            var result = await _userController.GetAllUsers();

            var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal(unexpectedErrorMessage, statusCodeResult.Value);
        }


    }
}