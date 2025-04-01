using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using TesteTécnicoIdeal.API.Controllers;
using TesteTécnicoIdeal.API.Exceptions;
using TesteTécnicoIdeal.API.Services;

namespace TesteTécnicoIdeal.UnitTests.Tests.UserControllerTests
{
    public class DeleteUserTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly UserController _userController;

        public DeleteUserTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _userController = new UserController(_userServiceMock.Object);
        }

        [Fact]
        public async Task Returns_Ok()
        {
            int userId = 1;
            _userServiceMock.Setup(service => service.DeleteUser(userId)).Returns(Task.CompletedTask);

            var result = await _userController.Delete(userId);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Returns_StatusCode400()
        {
            int invalidUserId = 1;
            CustomExceptions customError = new CustomExceptions("expected error message");
            _userServiceMock.Setup(service => service.DeleteUser(invalidUserId)).ThrowsAsync(customError);

            var result = await _userController.Delete(invalidUserId);

            var BadRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(customError.ErrorMessages,BadRequestResult.Value);
        }

        [Fact]
        public async Task Returns_StatusCode500()
        {
            int userId = 1;
            string unexpectedErrorMessage = "Um erro inesperado aconteceu(API).";
            _userServiceMock.Setup(service => service.DeleteUser(userId)).ThrowsAsync(new SystemException());

            var result = await _userController.Delete(userId);

            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal(unexpectedErrorMessage, statusCodeResult.Value);
        }



    }
}
