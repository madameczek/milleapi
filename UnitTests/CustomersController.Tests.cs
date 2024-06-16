using System.Data;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using milleapi.App.Interfaces;
using milleapi.App.Services;
using milleapi.Controllers;
using milleapi.Entities;
using milleapi.Models;
using Moq;

namespace UnitTests;

public class CustomersControllerTests
{
    [Fact]
    public async Task Action_Create_When_Repository_Returns_Customer_Should_Return_Status201Created()
    {
        // setup
        var mockRepository = new Mock<ICustomerRepository>(MockBehavior.Strict);
        mockRepository.Setup(repository => 
                repository.Add(It.IsAny<Customer>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => new Customer { Id = 1 });
        var customerService = new CustomerService(mockRepository.Object);
        var controller = new CustomersController(Mock.Of<ILogger<CustomersController>>(), customerService);
        var customerDto = new Mock<CreateCustomerDto>();

        // act
        var actionResult = await controller.Create(customerDto.Object) as CreatedResult;

        // assert
        actionResult!.Location.Should().Contain("customers/1");
        actionResult.StatusCode.Should().Be(StatusCodes.Status201Created);
    }
    
    [Fact]
    public async Task Action_Create_When_Repository_Throws_Exception_Should_Throw_Exception()
    {
        // setup
        var mockRepository = new Mock<ICustomerRepository>(MockBehavior.Strict);
        mockRepository.Setup(repository => 
                repository.Add(It.IsAny<Customer>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception());
        var customerService = new CustomerService(mockRepository.Object);
        var controller = new CustomersController(Mock.Of<ILogger<CustomersController>>(), customerService);

        // act
        Func<Task> act = () => controller.Create(new CreateCustomerDto());

        // assert
        await act.Should().ThrowAsync<Exception>();
    }
    
    [Fact]
    public async Task Action_Get_When_Repository_Throws_RowNotInATableException_Should_Throw_RowNotInATableException()
    {
        var mockRepository = new Mock<ICustomerRepository>(MockBehavior.Strict);
        mockRepository.Setup(repository => 
            repository.Get(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new RowNotInTableException("Customer not found"));
        var customerService = new CustomerService(mockRepository.Object);
        var controller = new CustomersController(Mock.Of<ILogger<CustomersController>>(), customerService);

        Func<Task> act = () => controller.Get(1);

        await act.Should().ThrowAsync<RowNotInTableException>()
            .WithMessage("Customer not found");
    }
    
    [Fact]
    public async Task Action_Get_When_Repository_Returns_Customer_Should_Return_Status200OK()
    {
        var mockRepository = new Mock<ICustomerRepository>();
        mockRepository.Setup(x => 
                x.Get(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(It.IsAny<Customer>());
        var customerService = new CustomerService(mockRepository.Object);
        var controller = new CustomersController(Mock.Of<ILogger<CustomersController>>(), customerService);

        var actionResult = await controller.Get(1) as OkResult;

        actionResult?.StatusCode.Should().Be(StatusCodes.Status200OK);
    }
    
    [Fact]
    public async Task Action_Update_When_Repository_Updates_Should_Return_Status204NoContent()
    {
        var mockRepository = new Mock<ICustomerRepository>();
        mockRepository.Setup(x => 
                x.Update(It.IsAny<Customer>(), It.IsAny<CancellationToken>()));
        var customerService = new CustomerService(mockRepository.Object);
        var controller = new CustomersController(Mock.Of<ILogger<CustomersController>>(), customerService);

        var actionResult = await controller.Update(1, new UpdateCustomerDto()) as NoContentResult;

        actionResult?.StatusCode.Should().Be(StatusCodes.Status204NoContent);
    }
    
    [Fact]
    public async Task Action_Delete_When_Repository_Deletes_Should_Return_Status204NoContent()
    {
        var mockRepository = new Mock<ICustomerRepository>();
        mockRepository.Setup(x => 
            x.Update(It.IsAny<Customer>(), It.IsAny<CancellationToken>()));
        var customerService = new CustomerService(mockRepository.Object);
        var controller = new CustomersController(Mock.Of<ILogger<CustomersController>>(), customerService);

        var actionResult = await controller.Delete(1) as NoContentResult;

        actionResult?.StatusCode.Should().Be(StatusCodes.Status204NoContent);
    }
}