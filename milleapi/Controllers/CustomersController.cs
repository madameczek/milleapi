using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using milleapi.App.Interfaces;
using milleapi.Models;
using milleapi.Shared.Mappers;
using milleapi.Shared.Pagination;

namespace milleapi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : Controller
{
    private readonly ILogger<CustomersController> _logger;
    private readonly ICustomerService _customerService;

    public CustomersController(ILogger<CustomersController> logger, ICustomerService customerService)
    {
        _logger = logger;
        _customerService = customerService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCustomerDto customerDto, CancellationToken ct = default)
    {
        var customer = await _customerService.Create(customerDto.ToCustomer(), ct);
            
        _logger.LogInformation(
            "{EndpointName} endpoint invoked. Customer: {Id} has been created", nameof(Create), customer.Id);
        return Created($"/customers/{customer.Id}", customer);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id, CancellationToken ct = default) =>
        Ok(await _customerService.Get(id, ct));

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery]PaginationRequestParameters paginationParams, 
        CancellationToken ct = default)
    {
        var pagedList = await _customerService.GetAll(paginationParams, ct);
        HttpContext.Response.Headers.Append("pagination", System.Text.Json.JsonSerializer.Serialize(
            new PaginationHeader
            {
                TotalCount = pagedList.TotalCount,
                TotalPages = pagedList.TotalPages,
                CurrentPage = pagedList.CurrentPage,
                PageSize = pagedList.PageSize,
                HasPrevious = pagedList.HasPrevious,
                HasNext = pagedList.HasNext
            }));
        return Ok(pagedList);
    }
    
     [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateCustomerDto dto, CancellationToken ct = default)
    {
        var customer = dto.ToCustomer();
        customer.Id = id;
        await _customerService.Update(customer, ct);
            
        _logger.LogInformation(
            "Endpoint: {EndpointName}. Message: Customer Id={Id} has been updated", nameof(Update), id);
        return NoContent();
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct = default)
    {
        await _customerService.Remove(id, ct);
            
        _logger.LogInformation(
            "Endpoint: {EndpointName}. Message: Customer Id={Id} has been marked as deleted", nameof(Delete), id);
        return NoContent();
    }
}