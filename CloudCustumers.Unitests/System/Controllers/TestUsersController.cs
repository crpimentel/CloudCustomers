using CloudCustomer.API.Controllers;
using CloudCustomer.API.Models;
using CloudCustumers.Unitests.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CloudCustumers.Unitests.System.Controllers
{
    public class TestUsersController
    {
        [Fact]
        public async Task Get_OnSucess_ReturnsStatusCode200()
        {
            //Arrange
            var mockUserService = new Mock<IUSersService>();
            mockUserService.Setup(service => service.GetAllUsers()).ReturnsAsync(UserFixture.GetTestUsers());
            var sut = new UsersController(mockUserService.Object);
            
            //Act
            var result = (OkObjectResult)await sut.Get();
            
            //Assert
            result.StatusCode.Should().Be(200);
        }
        [Fact]
        public async Task Get_OnSucess_InvokesUsersServiceExaclyOnce()
        {
            //Arrange
            var mockUserService = new Mock<IUSersService>();
            mockUserService.Setup(service=> service.GetAllUsers()).ReturnsAsync(new List<User>());  

            var sut = new UsersController(mockUserService.Object);

            //Act
            var result = await sut.Get();

            //Assert
            mockUserService.Verify(service => service.GetAllUsers(), Times.Once);
        }

        [Fact]
        public async Task Get_OnSucess_ReturnListOfUsers()
        {
            //Arrange
            var mockUserService = new Mock<IUSersService>();
            mockUserService.Setup(service => service.GetAllUsers()).ReturnsAsync(UserFixture.GetTestUsers());

            var sut = new UsersController(mockUserService.Object);

            //Act
            var result = (OkObjectResult)await sut.Get();

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            var objectResult = (OkObjectResult)result;
            objectResult.Value.Should().BeOfType<List<User>>();

        }

        [Fact]
        public async Task Get_OnNoUsersFound_Return404()
        {
            //Arrange
            var mockUserService = new Mock<IUSersService>();
            mockUserService.Setup(service => service.GetAllUsers()).ReturnsAsync(new List<User>());

            var sut = new UsersController(mockUserService.Object);

            //Act
            var result = await sut.Get();

            //Assert
            result.Should().BeOfType<NotFoundResult>();
            var objectResult = (NotFoundResult)result;
            objectResult.StatusCode.Should().Be(404);   
        }
    }
}
