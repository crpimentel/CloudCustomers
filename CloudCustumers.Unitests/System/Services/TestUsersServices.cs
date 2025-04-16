using CloudCustomer.API.Config;
using CloudCustomer.API.Models;
using CloudCustumers.Unitests.Fixtures;
using CloudCustumers.Unitests.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CloudCustumers.Unitests.System.Services
{
    public class TestUsersServices
    {
        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesHttpGetRequest()
        {
            //Arrange
            var expectedResponse = UserFixture.GetTestUsers();
            var endPoint = "https://example.com";
            var config = Options.Create(new UserApiOptions
            {
                EndPoint = endPoint
            });
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);
            var sut = new UsersServices(httpClient, config);
            //Act
            await sut.GetAllUsers();
            //assert
            handlerMock.Protected()
                .Verify("SendAsync",
                    Times.Exactly(1),
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                    ItExpr.IsAny<CancellationToken>());
        }
        [Fact]
        public async Task GetAllUsers_WhenHits404_ReturnEmptyListOfUsers()
        {
            //Arrange
            var handlerMock = MockHttpMessageHandler<User>.SetupReturn404();
            var httpClient = new HttpClient(handlerMock.Object);
            var endPoint = "https://example.com";
            var config = Options.Create(new UserApiOptions
            {
                EndPoint = endPoint
            });
            var sut = new UsersServices(httpClient, config);
            //Act
            var result = await sut.GetAllUsers();
            //assert
            result.Count.Should().Be(0);
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnListOfExpectedSize()
        {
            //Arrange
            var expectedResponse = UserFixture.GetTestUsers();
            var endPoint = "https://example.com/Users";
            var config = Options.Create(new UserApiOptions
            {
                EndPoint = endPoint
            });
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);
            var sut = new UsersServices(httpClient,config);
            //Act
            var result = await sut.GetAllUsers();
            //assert
            result.Count.Should().Be(expectedResponse.Count);
        }
        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokeConfigureExternalUrl()
        {
            //Arrange
            var expectedResponse = UserFixture.GetTestUsers();
            var endPoint = "https://example.com/Users";
            var config = Options.Create(new UserApiOptions
            {
                EndPoint = endPoint
            });
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse, endPoint);
            var httpClient = new HttpClient(handlerMock.Object);

           

            var sut = new UsersServices(httpClient, config);
            //Act
            var result = await sut.GetAllUsers();
            var uri = new Uri(endPoint);
            //assert
            handlerMock.Protected()
                .Verify("SendAsync",
                    Times.Exactly(1),
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get
                    && req.RequestUri== uri),
                    ItExpr.IsAny<CancellationToken>());
        }
    }
}
