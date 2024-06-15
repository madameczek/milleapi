using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using milleapi.App.Interfaces;
using milleapi.Controllers;
using milleapi.Entities;
using milleapi.Models;
using Moq;

namespace UnitTests;

public class CustomersControllerTests
{
    [Fact]
    public async Task Test_Create_StatusCode201()
    {
        var mockedRepository = new Mock<ICustomerService>(MockBehavior.Strict);
        mockedRepository.Setup(x =>
                x.Create(It.IsAny<Customer>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => new Customer { Id = 1 });
        var controller = new CustomersController(Mock.Of<ILogger<CustomersController>>(), mockedRepository.Object);

        var actionResult = await controller.Create(new CreateCustomerDto()) as CreatedResult;

        actionResult!.Location.Should().Contain("customers/1");
        actionResult.StatusCode.Should().Be(StatusCodes.Status201Created);
    }
    
    [Fact]
    public async Task Test_Create_StatusCode400()
    {
        var mockRepository = new Mock<ICustomerService>(MockBehavior.Strict);
        mockRepository.Setup(x => 
                x.Create(It.IsAny<Customer>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new ArgumentException());
        var controller = new CustomersController(Mock.Of<ILogger<CustomersController>>(), mockRepository.Object);

        Func<Task> act = () => controller.Create(new CreateCustomerDto());

        await act.Should().ThrowAsync<ArgumentException>();
    }
    
    [Fact]
    public async Task Test_Create_StatusCode503()
    {
        var mockRepository = new Mock<ICustomerService>();
        mockRepository.Setup(x => 
            x.Create(It.IsAny<Customer>(), It.IsAny<CancellationToken>()))
            .Throws(new Exception());
        var controller = new CustomersController(Mock.Of<ILogger<CustomersController>>(), mockRepository.Object);

        Func<Task> act = () => controller.Create(new CreateCustomerDto());

        await act.Should().ThrowAsync<Exception>();
    }
}