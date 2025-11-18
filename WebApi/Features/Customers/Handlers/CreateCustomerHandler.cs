using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using WebApi.Features.Customers.Commands;
using WebApi.Models.Responses;

namespace WebApi.Features.Customers.Handlers;

public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, CustomerResponse>
{
    private readonly AppDbContext _context;

    public CreateCustomerHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CustomerResponse> Handle(CreateCustomerCommand request, CancellationToken ct)
    {
        var customer = new Customer
        {
            FirstName = request.Request.FirstName ?? string.Empty,
            LastName = request.Request.LastName,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync(ct);

        return new CustomerResponse(
            customer.Id,
            customer.FirstName,
            customer.LastName,
            customer.CreatedAt);
    }
}
