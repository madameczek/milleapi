using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using milleapi.App.Interfaces;
using milleapi.Controllers;
using milleapi.Models;
using Moq;

namespace UnitTests;

public class CustomersControllerTests
{
    [Fact]
    public async Task Test_Create_StatusCode200()
    {
        var mockedRepository = new Mock<ICustomerService>(MockBehavior.Strict);
        mockedRepository.Setup(x => 
                x.Create(It.IsAny<CustomerDto>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);
        var controller = new CustomersController(Mock.Of<ILogger<CustomersController>>(), mockedRepository.Object);

        var actionResult = await controller.Create(new CustomerDto()) as CreatedResult;

        actionResult!.Location.Should().Contain("customers/1");
        actionResult.StatusCode.Should().Be(StatusCodes.Status201Created);
    }
    
    [Fact]
    public async Task Test_Create_StatusCode404()
    {
        var mockRepository = new Mock<ICustomerService>(MockBehavior.Strict);
        mockRepository.Setup(x => 
                x.Create(It.IsAny<CustomerDto>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new ArgumentException());
        var controller = new CustomersController(Mock.Of<ILogger<CustomersController>>(), mockRepository.Object);

        var actionResult = await controller.Create(new CustomerDto()) as BadRequestObjectResult;

        actionResult.Should().NotBeNull();
        actionResult!.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
    }
    
    [Fact]
    public async Task Test_Create_StatusCode503()
    {
        var mockRepository = new Mock<ICustomerService>();
        mockRepository.Setup(x => 
            x.Create(It.IsAny<CustomerDto>(), It.IsAny<CancellationToken>()))
            .Throws(new Exception());
        var controller = new CustomersController(Mock.Of<ILogger<CustomersController>>(), mockRepository.Object);

        var actionResult = await controller.Create(new CustomerDto()) as ObjectResult;

        actionResult.Should().NotBeNull();
        actionResult!.StatusCode.Should().Be(StatusCodes.Status503ServiceUnavailable);
    }
}