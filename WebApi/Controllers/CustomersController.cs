using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Features.Customers.Commands;
using WebApi.Features.Customers.Queries;
using WebApi.Models.Requests;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromServices] IMediator mediator,
        [FromBody] CreateCustomerRequest request,
        CancellationToken ct = default)
    {
        var result = await mediator.Send(new CreateCustomerCommand(request), ct);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(
        [FromServices] IMediator mediator,
        int id,
        CancellationToken ct = default)
    {
        var result = await mediator.Send(new GetCustomerQuery(id), ct);
        return Ok(result);
    }

    /// <summary>
    /// Get all customers with their orders
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromServices] IMediator mediator,
        CancellationToken ct = default)
    {
        var result = await mediator.Send(new GetAllCustomersQuery(), ct);
        return Ok(result);
    }

    /// <summary>
    /// Update customer. Can undelete
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        [FromServices] IMediator mediator,
        int id,
        [FromBody] UpdateCustomerRequest request,
        CancellationToken ct = default)
    {
        await mediator.Send(new UpdateCustomerCommand(id, request), ct);
        return NoContent();
    }

    /// <summary>
    /// Soft delete customer
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> SoftDelete(
        [FromServices] IMediator mediator,
        int id,
        CancellationToken ct = default)
    {
        await mediator.Send(new DeleteCustomerCommand(id), ct);
        return NoContent();
    }
}