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
    public class GetUserByIdTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly UserController _userController;
        public GetUserByIdTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _userController = new UserController(_userServiceMock.Object);
        }

        [Fact]
        public async Task Returns_Ok()
        {
            var user = new User { Id = 1, Name = "Name Test", Surname = "Surname Test", PhoneNumber = "99999999" };
            var userId = user.Id;
            _userServiceMock.Setup(service => service.GetUserById(userId)).ReturnsAsync(user);

            var result = await _userController.GetUserById(userId);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(user, okResult.Value);
        }

        [Fact]
        public async Task Returns_StatusCode400()
        {
            int NonExistentUser_Id = 9999;
            CustomExceptions customError = new CustomExceptions("expected error message");
            _userServiceMock.Setup(service => service.GetUserById(NonExistentUser_Id)).ThrowsAsync(customError);

            var result = await _userController.GetUserById(NonExistentUser_Id);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(customError.ErrorMessages, badRequestResult.Value);
        }

        [Fact]
        public async Task Returns_StatusCode500()
        {
            string unexpectedErrorMessage = "Um erro inesperado aconteceu(API).";
            int UnexpectedError_Id = 0;
            _userServiceMock.Setup(service => service.GetUserById(UnexpectedError_Id)).ThrowsAsync(new SystemException());

            var result = await _userController.GetUserById(UnexpectedError_Id);

            var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal(unexpectedErrorMessage, statusCodeResult.Value);
        }






    }
}
